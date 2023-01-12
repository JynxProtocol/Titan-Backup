using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using Topshelf;
using Host = Microsoft.Extensions.Hosting.Host;

namespace Titan.API
{
    public class Program
    {
        public static void Main(string[] args)
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

            HostFactory.Run(HostConfiguration =>
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

                HostConfiguration.Service<WebAPIService>(ServiceConfiguaration =>
                {
                    ServiceConfiguaration.ConstructUsing(settings => new WebAPIService());

                    ServiceConfiguaration.WhenStarted((s, hostControl) =>
                    {
                        Log.Information("Starting Service");
                        return s.Start(hostControl);
                    });

                    ServiceConfiguaration.WhenStopped((s, hostControl) =>
                    {
                        Log.Information("Stopping Service");
                        Log.CloseAndFlush();
                        return s.Stop(hostControl);
                    });




                });

                HostConfiguration.RunAsLocalSystem();

                HostConfiguration.SetDescription("Titan.API");
                HostConfiguration.SetDisplayName("Titan.API.Service");
                HostConfiguration.SetServiceName("Titan.API.Service");
            });

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:25458");
                });


    }
}
