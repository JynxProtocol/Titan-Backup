using Titan.Data;

namespace Titan.Functions
{
    public class GRN
    {
        //public static void LogData(string logModule, string LogDetails, int LogLevel)
        //{
        //    var mydata = new TitanEntities();
        //    int logginglevel = mydata.Settings.Select(log => log.LogLevel).First();

        //    if (LogLevel <= logginglevel)
        //    {
        //        var newlog = new Log();
        //        newlog.logModule = logModule;
        //        newlog.logDetails = LogDetails;
        //        newlog.logDate = System.DateTime.Now;
        //        newlog.UsrName = "System";
        //        mydata.Logs.Add(newlog);
        //        mydata.SaveChanges();

        //        if (logginglevel == 2)
        //        {
        //            //Do debug output here - Replace for Web
        //            //Console.WriteLine(newlog.logDate + " : " + logModule + " : " + LogDetails);
        //        }

        //        if (LogLevel == 0)
        //        {
        //            //Do Email Warning here
        //        }
        //    }
        //}

        public static void UpdateContract()
        {
            try
            {
            }
            catch
            {
            }
        }

        public static void CreateOrder()
        {
        }

        public static void AddOrderHeader()
        {
        }

        public static void AddOrderDetails()
        {
        }
    }
}