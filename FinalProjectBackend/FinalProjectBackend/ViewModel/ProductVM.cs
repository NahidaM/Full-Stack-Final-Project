using FinalProjectBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.ViewModel
{
    public class ProductVM
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; } 
    }
}
