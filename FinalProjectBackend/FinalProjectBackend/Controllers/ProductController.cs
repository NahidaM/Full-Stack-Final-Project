using FinalProjectBackend.DAL;
using FinalProjectBackend.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            ProductVM product = new ProductVM
            {
                Categories = _context.Categories
                                                .Where(c => c.Deleted == false)
                                                .ToList(),
                Products = _context.Products
                                            .Include(p => p.Category)
                                            .Where(p => p.Deleted == false && p.Count > 0)
                                            .Take(8)
                                            .ToList()
            };
            ViewBag.ProCount = _context.Products
                                                .Where(p => p.Deleted == false && p.Count > 0)
                                                .Count();
            return View(product);
        }

    }
}
