using FinalProjectBackend.DAL;
using FinalProjectBackend.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public ProductViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int take)
        {
            ProductVM productVM = new ProductVM
            {
                Categories = _context.Categories.Where(c => c.Deleted == false).ToList(),
                Products = _context.Products.Include(pro => pro.Category).Where(p => p.Deleted == false).Take(take).ToList()
            };
            return View(await Task.FromResult(productVM)); 
        }
    }
}
