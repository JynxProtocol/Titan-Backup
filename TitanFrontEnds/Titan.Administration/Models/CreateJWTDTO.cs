using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TitanAPIAdminConnection
{
    public class CreateJWTDTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Underscore prefix is valid for underlying data store")]
        public Dictionary<string, bool> _Features { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Expiration { get; set; }

        public List<FeatureBooleanKeyValuePair> Features { get; set; }

        public List<StringTrinaryKeyValuePair> FeatureAreas { get; set; }

        public string Name { get; set; }
    }
}
