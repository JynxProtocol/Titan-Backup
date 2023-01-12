using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Titan.Models.AccessControl.Roles
{
    public class Role
    {
        public virtual int ID { get; set; }
        public virtual string RoleName { get; set; }
    }
}
