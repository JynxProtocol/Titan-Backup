using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Titan.Models.AccessControl;

namespace Titan.Models.Authentication
{
    public class CreateJWT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Underscore prefix is valid for underlying data store")]
        public Dictionary<string, bool> _Features { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Expiration { get; set; }

        public KeyValuePair<Feature, bool>[] Features { get; set; }

        public KeyValuePair<string, Trinary>[] FeatureAreas { get; set; }

        public string Name { get; set; }
    }
}
