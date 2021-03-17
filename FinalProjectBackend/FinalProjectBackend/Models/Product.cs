using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public string Image { get; set; }
        public int Count { get; set; }
        [Required] 
        public double Price { get; set; }
        [Required, DefaultValue(false)]
        public bool Deleted { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required(ErrorMessage = "Please add photo!"), NotMapped]
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Please add photo!"), NotMapped]
        public IFormFile[] Photos { get; set; } 
    }
}
