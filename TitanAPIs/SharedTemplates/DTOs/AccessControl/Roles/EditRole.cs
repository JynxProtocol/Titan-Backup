using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Titan.Models.AccessControl.Roles
{
    public class EditRole : ViewRole
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Underscore prefix is valid for underlying data store")]
        public Dictionary<string, bool> _Features { get; set; }
    }
}
