using System.Collections.Generic;
using Titan.Models.AccessControl;

namespace Titan.Models.User
{

    // transport class used to return the features a user can access, as well as feature areas (in Trinary) 
    public class UserEffectiveAccess
    {
        public KeyValuePair<Feature, bool>[] Features { get; set; }

        public KeyValuePair<string, Trinary>[] FeatureAreas { get; set; }
    }
}
