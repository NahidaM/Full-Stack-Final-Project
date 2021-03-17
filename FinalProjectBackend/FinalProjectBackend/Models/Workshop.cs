﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.Models
{
    public class Workshop
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MoreInfo { get; set; }
        [Required, StringLength(255)]
        public string Image { get; set; } 
    }
}
