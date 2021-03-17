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
    public class Slider
    {   
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Image { get; set; }
        [Required(ErrorMessage = "Please add photo!"), NotMapped]
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Please add photo!"), NotMapped]
        public IFormFile[] Photos { get; set; }
    }
}
