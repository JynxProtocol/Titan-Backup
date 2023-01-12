using Sage.Accounting;
using System;
using System.Linq;
using Topshelf.Owin;

namespace Sage.Api
{

    internal class SageService : IOwinService
    {
        public static Sage.Accounting.Application SageApplication = new Sage.Accounting.Application();

        public bool Stop()
        {
            DisconnectFromSage();

            //Generic.LogVerbose(new GenericLogDetails(null, "Service Stopped") { AdditionalInfo = new System.Collections.Generic.Dictionary<string, object> { { "Sage", "Service" } } });

            return true;
        }

        public bool Start()
        {
            //TitanLogging.TitanLogging.SetLoggers();

            ConnectToSage();

            //Generic.LogVerbose(new GenericLogDetails(null, "Service Started") { AdditionalInfo = new System.Collections.Generic.Dictionary<string, object> { { "Sage", "Service" } } });

            return true;
        }


        public static void ConnectToSage()
        {
            try
            {
                // connect to sage sdk
                SageApplication.Connect();

                // select company by name
                string CompanyName = "sabreraillive"; // LIVE ONE LOWER CASE ONLY

                var Company = SageApplication.Companies.Cast<Company>()
                    .Where(s => s.Name.IndexOf(CompanyName, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    .FirstOrDefault();

                SageApplication.ActiveCompany = Company;

                //Generic.LogVerbose(new GenericLogDetails(null, "Connect To Sage - connected To" + app.ActiveCompany.Name) { AdditionalInfo = new System.Collections.Generic.Dictionary<string, object> { { "Sage", "Service" } } });


            }
            catch (Exception ex)
            {
                //Catch any error
                //Generic.LogError(new GenericLogDetails(null, "Could Not Connect to Sage: Error is:" + ex.Message) { AdditionalInfo = new System.Collections.Generic.Dictionary<string, object> { { "Sage", "Service" } } });

                throw;
            }
            finally
            {

            }
        }

        public static void DisconnectFromSage()
        {
            Console.WriteLine("Disconnecting from Sage");
            SageApplication.Disconnect();

            //Generic.LogVerbose(new GenericLogDetails(null, "Disconected from Sage") { AdditionalInfo = new System.Collections.Generic.Dictionary<string, object> { { "Sage", "Service" } } });
        }
    }
}
