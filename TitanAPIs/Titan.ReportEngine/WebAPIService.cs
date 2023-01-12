using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;

namespace Titan.ReportEngine
{
    public class WebAPIService : Topshelf.ServiceControl
    {
        private Task WebServer;
        private CancellationTokenSource WebServerCancellationTokenSource = new CancellationTokenSource();

        public bool Start(HostControl hostControl)
        {
            var args = Environment.GetCommandLineArgs();

            // setup the webserver as we would in Program.Main, only asynchronously 
            WebServer = Program.CreateHostBuilder(args).Build().RunAsync(WebServerCancellationTokenSource.Token);

            // return false on an aborted startup
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            // stop the webserver by sending a cancel signal
            WebServerCancellationTokenSource.Cancel();

            // wait for the webserver task to complete. If it takes longer than 10 seconds, time out
            if (WebServer.Wait(TimeSpan.FromSeconds(10)))
            {
                // stopping the server completed successfully
                return true;
            }
            else
            {
                // Stopping the server took more than 10 seconds, so report that the shutdown failed
                return false;
            }
        }
    }
}
