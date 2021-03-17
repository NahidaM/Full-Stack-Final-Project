using FinalProjectBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.ViewModel
{
    public class UserRoleVM
    {
        public AppUser User { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
