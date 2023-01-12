using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using Host = Microsoft.Extensions.Hosting.Host;
using Serilog;

namespace Titan.Stock
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

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Log.Information("Starting Web Application");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
