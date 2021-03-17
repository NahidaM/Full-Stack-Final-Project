using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }  
        public DateTime Time { get; set; }
        public bool IsDeleted { get; set; } 
        public int CategoryEventId { get; set; } 
        public virtual CategoryEvent CategoryEvent { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }

    }
}
