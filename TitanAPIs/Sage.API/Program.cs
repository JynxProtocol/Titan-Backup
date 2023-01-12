using Sage.Api.Data;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Topshelf;
using Topshelf.Owin;

namespace Sage.Api
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            {
                var ApplicationName = System.Reflection.Assembly.GetEntryAssembly()?.GetName()?.Name;

                var columnOptions = new ColumnOptions
                {
                    AdditionalColumns = new Collection<SqlColumn>
                {
                    new SqlColumn
                    {ColumnName = "ApplicationName", PropertyName = "ApplicationName", DataType = SqlDbType.NVarChar, DataLength = 64},

                }
                };

                //TitanLogging.TitanLogging.SetLoggers();

                HostFactory.Run(c =>
                {


                    Log.Logger = new LoggerConfiguration()
                        .Enrich.WithProperty("ApplicationName", ApplicationName)
                        .Enrich.WithMachineName()
                        .WriteTo.Console()
                        .WriteTo.MSSqlServer(
                            connectionString: "Server=.\\sagedev;Database=LogDb;Integrated Security=SSPI;",
                            sinkOptions: new MSSqlServerSinkOptions { TableName = "LogEvents", AutoCreateSqlTable = true },
                            columnOptions: columnOptions)
                        .WriteTo.File("logs/logs.txt",
                            restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                            rollingInterval: RollingInterval.Day)
                        .WriteTo.File("logs/errorlog.txt", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
                        .CreateLogger();

                    c.Service<SageService>(s =>
                    {
                        s.ConstructUsing(() => new SageService());

                        s.WhenStarted((service, control) =>
                        {
                            Log.Information("Starting Service");
                            return service.Start();
                        });

                        s.WhenStopped((service, control) =>
                        {
                            Log.Information("Stopping Service");
                            Log.CloseAndFlush();
                            return service.Stop();
                        });

                        //Get Local IP Address


                        s.OwinEndpoint(app =>
                        {
                            app.Domain = "localhost";
                            app.Port = 9000;

                            app.ConfigureHttp(httpConfiguration =>
                            {
                                httpConfiguration.Services.Replace(typeof(IExceptionHandler),
                                    new CustomExceptionHandler());

                                httpConfiguration.MapHttpAttributeRoutes();
                                SwaggerConfig.Register(httpConfiguration);
                                httpConfiguration.EnsureInitialized();
                            });

                            c.RunAsLocalSystem();
                            c.SetDescription("Titan.Sage.Service");
                            c.SetDisplayName("Titan.Sage.Service");
                            c.SetServiceName("Titan.Sage.Service");

                            // Initialise entity framework connection (takes ages the first time)
                            var time = DateTime.Now;
                            Console.WriteLine("Initialising database connection (EntityFramework)");
                            new SabreLive().Database.Initialize(false);
                            Console.WriteLine($"Initialized database connection, took {(DateTime.Now - time).TotalSeconds} seconds");
                        });
                    });
                });
            }
        }
    }
}

