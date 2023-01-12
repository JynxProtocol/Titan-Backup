using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Titan.Models.AccessControl.Roles
{
    // transport class representing Roles, with the features they can access, as well as feature areas (in Trinary)
    public class ViewRole : Role
    {
        public KeyValuePair<Feature, bool>[] Features { get; set; }

        public KeyValuePair<string, Trinary>[] FeatureAreas { get; set; }

        public List<string> Users { get; set; }
    }
}
