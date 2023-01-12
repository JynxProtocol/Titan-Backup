using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Sage.Api.Models.Tracking
{
    public class WOTrackingDTO
    {
        public string WorksOrderNumber { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Operation Step { get; set; }
        public bool Finished { get; set; }
    }
}
