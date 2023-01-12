using System.Collections.Generic;

namespace Titan.API.Models.AWK
{
    public class PartsList
    {
        public int PLHID { get; set; }
        public string Title { get; set; }
        public string ProductGroup { get; set; }

        public List<PartsListItem> PartsListItems { get; set; }
        public List<PartsListCatItem> PartsListCats { get; set; }
    }
}
