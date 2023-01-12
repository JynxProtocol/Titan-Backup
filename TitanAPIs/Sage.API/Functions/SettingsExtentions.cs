using Sage.Api.Data;
using Sage.Common.LinqExtensions;
using System.Linq;

namespace Sage.Api.Functions
{
    internal static class SettingsExtentions
    {
        public static StockSetting StockSetting(this TitanEntities Titan)
        {
            return Titan.StockSettings.Single();
        }
    }
}
