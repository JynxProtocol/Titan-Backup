using System.Collections.Generic;

namespace Titan.API.Models.AWK
{
    public class PartsListCat
    {
        public int PLHID { get; set; }
        public string Title { get; set; }

        public List<PartsListCatItem> PartsListCatItems { get; set; }
    }
}
