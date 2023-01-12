using System;

namespace Titan.API.Models.AWK
{
    public class PartsListCatItem
    {
        public int CATID { get; set; }
        public Nullable<int> PLHID { get; set; }
        public Nullable<int> SageStkID { get; set; }
        public string CatNumber { get; set; }
        public string Description { get; set; }
    }
}
