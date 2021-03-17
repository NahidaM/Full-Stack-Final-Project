using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.Models
{
    public class CategoryEvent
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public bool IsDeleted { get; set; }   
    }
}
