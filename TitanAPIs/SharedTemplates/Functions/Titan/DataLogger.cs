using System.Linq;
using Titan.Data;

namespace Titan.Functions
{
    public class DataLogger
    {
        public static void LogData(string logModule, string LogDetails, int LogLevel)
        {
            var mydata = new TitanEntities();
            int logginglevel = mydata.Settings.Select(log => log.LogLevel).First();

            if (LogLevel <= logginglevel)
            {
                var newlog = new Log
                {
                    logModule = logModule,
                    logDetails = LogDetails,
                    logDate = System.DateTime.Now,
                    UsrName = "System"
                };
                mydata.Logs.Add(newlog);
                mydata.SaveChanges();

                if (logginglevel == 2)
                {
                    //Do debug output here - Replace for Web
                    //Console.WriteLine(newlog.logDate + " : " + logModule + " : " + LogDetails);
                }

                if (LogLevel == 0)
                {
                    //Do Email Warning here
                }
            }
        }
    }
}