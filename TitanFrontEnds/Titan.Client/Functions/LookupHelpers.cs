using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using TitanAPIConnection;

namespace Titan.Client.Functions
{
    /// <summary>
    /// LookupHelpers are used to create dropdowns on UIs. They create SelectLists from 
    /// information returned by APIs, or just from value types such as integers or booleans
    /// </summary>
    public static class LookupHelpers
    {
        /// <summary>
        /// Converts a list of property value options returned by an API to a SelectList 
        /// that can be displayed on the front-end
        /// </summary>
        /// <param name="dropdownOptions">The list of options the user can choose between</param>
        public static IEnumerable<SelectListItem> ToDropDown(
            this ICollection<DropdownOption> dropdownOptions)
        {
            return dropdownOptions.Select(Option => new SelectListItem
            {
                Text = Option.Text,
                Value = Option.Value
            });
        }

        /// <summary>
        /// Converts a list of property value options returned by an API to a SelectList 
        /// that can be displayed on the front-end
        /// </summary>
        /// <param name="dropdownOptions">The list of options the user can choose between</param>
        public static IEnumerable<SelectListItem> ToDropDown(
            this ICollection<LookUpAWKWorkRequired> dropdownOptions)
        {
            return dropdownOptions.Select(Option => new SelectListItem
            {
                Text = Option.LookupText,
                Value = Option.LookupText
            });
        }

        /// <summary>
        /// Converts a list of property value options returned by an API to a SelectList 
        /// that can be displayed on the front-end
        /// </summary>
        /// <param name="dropdownOptions">The list of options the user can choose between</param>
        public static IEnumerable<SelectListItem> ToDropDown(
            this ICollection<LookUpAWKFault> dropdownOptions)
        {
            return dropdownOptions.Select(Option => new SelectListItem
            {
                Text = Option.LookupText,
                Value = Option.LookupText
            });
        }

        /// <summary>
        /// This SelectList is used to display yes/no options for an integer property
        /// (a lot of older code in Titan uses integers to store boolean values).
        /// This means the user can see a readable representation of the value
        /// </summary>
        public static List<SelectListItem> YesNoFromInt = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Text = "Yes",
                Value = "1"
            },
            new SelectListItem
            {
                Text = "No",
                Value = "0"
            }
        };

        /// <summary>
        /// This SelectList is used to display yes/no options for a boolean property
        /// This means the user can see a readable representation of the value
        /// </summary>
        public static List<SelectListItem> YesNoFromBool = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Text = "Yes",
                Value = "true"
            },
            new SelectListItem
            {
                Text = "No",
                Value = "false"
            }
        };

        /// <summary>
        /// This SelectList is used to display acknowledgement options for an integer property
        /// (a lot of older code in Titan uses integers to store boolean values).
        /// This means the user can see a readable representation of the value
        /// </summary>
        public static List<SelectListItem> AcknowledgedOptions = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Text = "Acknowledged",
                Value = "1"
            },
            new SelectListItem
            {
                Text = "Not Acknowledged",
                Value = "0"
            }
        };
    }
}
