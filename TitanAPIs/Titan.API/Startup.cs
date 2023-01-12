using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReportEngineConnection;
using SageAPIConnection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading;
using Titan.Functions;
using TitanAdminConnection;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

namespace Titan.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IHttpContextAccessor HttpContextAccessor { get; private set; }

        public static IConfiguration Configuration { get; private set; }

        // expedite lock, prevents running twice at the same time
        public static readonly SemaphoreSlim ExpediteLock = new(1, 1);

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // remove serialiser for xml, as we don't need it
            services.AddMvcCore(options =>
            {
                options.OutputFormatters
                    .RemoveType(typeof(XmlDataContractSerializerOutputFormatter));
            });

            // add DBContext
            services.AddDbContext<SAGEContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SAGE")));
            services.AddDbContext<TitanContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TITAN")));

            // add JWT authentication, using secret key
            services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = Configuration["TokenValidIssuer"],
                    ValidAudience = Configuration["TokenValidAudience"],
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenSecretKey"]))
                };
            }
            );

            services.AddControllers(c =>
            {
                c.ReturnHttpNotAcceptable = true;
                c.AllowEmptyInputInBodyModelBinding = true;
            });

            // add HttpContextAccessor service, which allows for accessing the current
            // requests context inside other services 
            // used to get the JWT for API requests
            services.AddHttpContextAccessor();

            // add the API connections this app uses as services
            AddAPIConnections(services);

            // add HttpClient, used to make requests to APIs
            services.AddTransient((s) =>
            {
                var USEDANGEROUSIGNORESSL = Configuration["USEDANGEROUSIGNORESSL"] == "true";

                HttpClientHandler handler = USEDANGEROUSIGNORESSL ? new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback =
                       (httpRequestMessage, cert, cetChain, policyErrors) =>
                       {
                           return true;
                       }
                } : new HttpClientHandler();

                return new HttpClient(handler);
            });

            // add email client as service, with configuration from database
            services.AddScoped((s) =>
            {
                var Titan = s.GetRequiredService<TitanContext>();

                string Server;
                int Port;

                if (Titan.FeatureFlags.ShouldRunFeature("TestEmailClient"))
                {
                    Server = Titan.Settings.AsNoTracking().Single().TestEmailServer;
                    Port = Titan.Settings.AsNoTracking().Single().TestEmailPort ?? 25;
                }
                else
                {
                    Server = Titan.Settings.AsNoTracking().Single().EmailServer;
                    Port = Titan.Settings.AsNoTracking().Single().EmailPort ?? 25;
                }

                var Client = new SmtpClient(Server, Port);

                if (Titan.FeatureFlags.ShouldRunFeature("SaveEmailsToDisk"))
                {
                    Client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    Client.PickupDirectoryLocation = @"C:\Emails\" +
                        DateTime.Now.ToString("yyyy-MM-dd");
                    Directory.CreateDirectory(Client.PickupDirectoryLocation);
                }

                return Client;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Titan.API",
                    Version = "v1",
                    Description = "An ASP.NET Core Web API for Titan functions",

                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. " +
                    "Enter 'Bearer' [space] and then your token in the text input below",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                c.AddEnumsWithValuesFixFilters();

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]}");
            });

            services.AddSwaggerGenNewtonsoftSupport();

            //Generic.Log(new GenericLogDetails(null, $"Application started"));
        }

        /// <summary>
        /// This function adds the required API Connections for the application as services, 
        /// so they can be injected into Controllers, or accessed anywhere through DI 
        /// </summary>
        /// <param name="services">The service collection to add them to</param>
        private void AddAPIConnections(IServiceCollection services)
        {
            services.AddScoped((s) =>
            {
                var HttpClient = GetHttpClient(s);

                HttpClient.BaseAddress = new Uri(Configuration["SAGEAPIURL"]);

                // configure the HttpContext of the current request for the API client,
                // so we can refresh the users token if it expires
                return new SageAPI(HttpClient)
                {
                    HttpContext = s.GetRequiredService<IHttpContextAccessor>().HttpContext
                };
            });

            services.AddScoped((s) =>
            {
                var HttpClient = GetHttpClient(s);

                HttpClient.BaseAddress = new Uri(Configuration["AUTHAPIURL"]);

                // configure the HttpContext of the current request for the API client,
                // so we can refresh the users token if it expires
                return new TitanAdmin(HttpClient)
                {
                    HttpContext = s.GetRequiredService<IHttpContextAccessor>().HttpContext
                };
            });

            services.AddScoped((s) =>
            {
                var HttpClient = GetHttpClient(s);

                HttpClient.BaseAddress = new Uri(Configuration["ReportEngineURL"]);

                return new ReportEngine(HttpClient);
            });
        }

        /// <summary>
        /// This function configures a HttpClient with the current users auth token if there is one
        /// avaliable, fetching both the HttpContext and HttpClient from the service provider.
        /// </summary>
        /// <param name="s">The service provider</param>
        /// <returns>The configured HttpClient</returns>
        private HttpClient GetHttpClient(IServiceProvider s)
        {
            var HttpClient = s.GetRequiredService<HttpClient>();
            var HttpContext = s.GetRequiredService<IHttpContextAccessor>().HttpContext;

            var AuthToken = HttpContext?.GetTokenAsync("access_token").Result;

            if (AuthToken != null)
            {
                HttpClient.DefaultRequestHeaders.Add("Authorization",
                    "Bearer " + AuthToken);
            }

            return HttpClient;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IHostApplicationLifetime applicationLifetime, IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;

            app.UseExceptionHandler("/Error");

            app.UseSwagger();
            app.UseSwaggerUI(Options => Options.SwaggerEndpoint("/swagger/v1/swagger.json", "Titan.API v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            // authenticate and authorise user by JWT
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
