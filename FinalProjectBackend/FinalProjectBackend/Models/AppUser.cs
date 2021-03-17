using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.Models
{
    public class AppUser:IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActivated { get; set; }
    }
}
