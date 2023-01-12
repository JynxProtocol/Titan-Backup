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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using Titan.Data;
using Titan.Functions;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Titan.API.Administration
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JWTSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenSecretKey"]));
            JWTCreds = new SigningCredentials(JWTSigningKey, SecurityAlgorithms.HmacSha256);
        }

        public static IHttpContextAccessor HttpContextAccessor { get; private set; }
        public IConfiguration Configuration { get; }

        private static SymmetricSecurityKey JWTSigningKey { get; set; }
        public static SigningCredentials JWTCreds { get; private set; }

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
            services.AddDbContext<TitanAuthContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TITAN_AUTH")));
            services.AddDbContext<TitanContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TITAN")));

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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenSecretKey"])),
                    ValidateIssuerSigningKey = true
                };
            }
            );

            services.AddHttpContextAccessor();

            services.AddControllers(c =>
            {
                c.ReturnHttpNotAcceptable = true;
            })
            .AddControllersAsServices();

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
                    Title = "Titan.API.Administration",
                    Version = "v1",
                    Description = "An ASP.NET Core Web API for managing " +
                    "authentication within Titan",

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IHostApplicationLifetime applicationLifetime, IHttpContextAccessor httpContextAccessor)
        {
            //TitanLogging.TitanLogging.SetLoggers();
            //applicationLifetime.ApplicationStopping.Register(() => TitanLogging.TitanLogging.Dispose());

            HttpContextAccessor = httpContextAccessor;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/error");

            }

            app.UseSwagger();
            app.UseSwaggerUI(Options => Options.SwaggerEndpoint("/swagger/v1/swagger.json", "Titan.API.Administration v1"));


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
