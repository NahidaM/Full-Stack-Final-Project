using FinalProjectBackend.DAL;
using FinalProjectBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

namespace FinalProjectBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> categories = _context.Categories
                                                           .Where(c => c.Deleted == false)
                                                           .ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExist = _context.Categories
                                              .Where(c => c.Deleted == false)
                                              .Any(c => c.Name.ToLower() == category.Name.ToLower());
            if (isExist)
            {
                ModelState.AddModelError("Name", "This category already exists.");
                return View();
            }
            category.Deleted = false;
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category category = await _context.Categories
                                                         .FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category category = _context.Categories.Where(c => c.Id == id).FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category category = _context.Categories
                                                   .Where(c => c.Id == id)
                                                   .Include(c => c.Products)
                                                   .FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            category.Deleted = true;
            foreach (Product pro in category.Products)
            {
                pro.Deleted = true;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category category = _context.Categories.Where(c => c.Id == id).FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Category category)
        {
            if (category == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            Category ctg = _context.Categories
                                              .Where(c => c.Deleted == false)
                                              .FirstOrDefault(c => c.Id == category.Id);

            if (!(ctg.Name.ToLower() == category.Name.ToLower().Trim()))
            {
                if (ctg.Id != category.Id)
                {
                    ModelState.AddModelError("Name", "This category name already have!");
                    return View(category);
                }
            }

            ctg.Name = category.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
