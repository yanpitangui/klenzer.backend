using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Klenzer.WebApi.Controllers.Outputs
{
    public class UserOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
