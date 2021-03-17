using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.Models
{
    public class Category 
    {
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }
        [Required, DefaultValue(false)] 
        public bool Deleted { get; set; } 
        public virtual ICollection<Product> Products { get; set; }
    }
}
