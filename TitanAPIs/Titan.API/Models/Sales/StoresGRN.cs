using System.Collections.Generic;

namespace Titan.API.Models.Sales
{
    public class StoresGRN
    {
        public List<ReceivedPart> Parts { get; set; }
        public string GRNID { get; set; }
    }
}
