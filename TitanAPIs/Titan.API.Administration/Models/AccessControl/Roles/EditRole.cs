using System.Collections.Generic;

namespace Titan.Models.AccessControl.Roles
{
    /// <summary>
    /// A role with an edited set of features
    /// </summary>
    public class EditRole : ViewRole
    {
        /// <summary>
        /// An updated list of features and whether the role gives access to them
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Underscore prefix is valid for underlying data store")]
        public Dictionary<string, bool> _Features { get; set; }
    }
}
