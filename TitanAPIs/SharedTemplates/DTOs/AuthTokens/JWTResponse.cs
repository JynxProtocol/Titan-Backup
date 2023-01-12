using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Models.Authentication
{
    public class JWTResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Name { get; set; }
}
}
