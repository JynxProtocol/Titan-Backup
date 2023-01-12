using Ganss.Xss;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using Titan.Filters;
using TitanAPIAdminConnection;

namespace Titan.Administration
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(o =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                o.Filters.Add(new AuthorizeFilter(policy));
                o.Filters.Add(new TokenFilter());
            });//.AddMvcOptions(options => options.Filters.Add(new Functions.TokenFilter())); ;

            // add cookie authentication, to store the userid between requests
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.RequireAuthenticatedSignIn = false;
            }).AddCookie();

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

            // add HTMLSanitiser, used to sanitise HTML, removing non-format/style tags
            services.AddSingleton((s) =>
            {
                // removes html
                var HtmlSanitiser = new HtmlSanitizer();
                HtmlSanitiser.RemovingTag += (sender, args) =>
                {
                    if (!args.Tag.Matches("script"))
                    {
                        args.Tag.OuterHtml = HtmlSanitiser.Sanitize(args.Tag.InnerHtml);
                        args.Cancel = true;
                    }
                };
                return HtmlSanitiser;
            });
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

                HttpClient.BaseAddress = new Uri(Configuration["AUTHAPIURL"]);

                // configure the HttpContext of the current request for the API client,
                // so we can refresh the users token if it expires
                return new TitanAdmin(HttpClient)
                {
                    HttpContext = s.GetRequiredService<IHttpContextAccessor>().HttpContext
                };
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

            var AuthToken = HttpContext?.User?.FindFirst("token")?.Value;

            if (AuthToken != null)
            {
                HttpClient.DefaultRequestHeaders.Add("Authorization",
                    "Bearer " + AuthToken);
            }

            return HttpClient;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Error");
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
