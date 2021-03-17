using FinalProjectBackend.DAL;
using FinalProjectBackend.Extentions;
using FinalProjectBackend.Helpers;
using FinalProjectBackend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    { 
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(AppDbContext db, IWebHostEnvironment env)
        {
            _context = db;
            _env = env;
        }

        #region Index
        public ActionResult Index() 
        {
            List<Product> Products = _context.Products.Where(p => p.Deleted == false).ToList();
            return View(Products);
        }
        #endregion

        #region ProductDetail
        public async Task<ActionResult> Detail(int? id)
        {
            if (id == null)
            {
                NotFound();
            }
            Product Product = await _context.Products
                     .Include(p => p.Category)
                        .Where(p => p.Deleted == false && p.Id == id).FirstOrDefaultAsync();
            return View(Product);
        }
        #endregion

        #region ProductCreate
        public IActionResult Create()
        {
            ViewBag.Products = _context.Categories.Where(b => b.Deleted == false).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product Product, int? CategoryId)
        {
            ViewBag.Products = _context.Categories.Where(b => b.Deleted == false).ToList();
            Product newProduct = new Product();
            if (Product.Photo == null)
            {
                ModelState.AddModelError("Photo", "Shekil bolmesi bosh qala bilmez!");
                return View();
            }

            if (!Product.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                return View();
            }

            if (!Product.Photo.MaxSize(600))
            {
                ModelState.AddModelError("Photo", "Sheklin maksimum olcusu 200 kb ola biler");
                return View();
            }

            string folder = Path.Combine("img");
            string fileName = await Product.Photo.SaveImageAsync(_env.WebRootPath, folder);

            if (CategoryId == null)
            {
                ModelState.AddModelError("CategoryId", "Xahis edirik CategoryId secin!");
                ViewBag.SelectError = "Please select author";
                return View();
            }

            newProduct.Image = fileName;
            newProduct.Name = Product.Name;
            newProduct.Image = Product.Image;
            newProduct.Price = Product.Price;
            newProduct.Count = Product.Count;

            newProduct.CategoryId = (int)CategoryId;
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region ProductUpdate
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            ViewBag.Products = _context.Categories.Where(b => b.Deleted == false).ToList();
            Product Product = await _context.Products.Where(p => p.Deleted == false).FirstOrDefaultAsync(p => p.Id == id);
            if (Product == null) return NotFound();
            ViewBag.ProductsId = Product.Category.Id;
            return View(Product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Product Product, int? CategoryId)
        {
            ViewBag.Products = _context.Categories.Where(b => b.Deleted == false).ToList();
            Product newProduct = new Product();
            Product dbProduct = await _context.Products.Where(p => p.Deleted == false)
               .Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (dbProduct == null) return NotFound();
            ViewBag.CategoryId = dbProduct.Category.Id;

            if (Product.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }
                if (!Product.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                    return View();
                }

                if (!Product.Photo.MaxSize(600))
                {
                    ModelState.AddModelError("Photo", "Sheklin maksimum olcusu 200 kb ola biler");
                    return View();
                }

                string folder = Path.Combine("img"); 
                //Helper.DeletePhoto(_env.WebRootPath, folder, dbProduct.Image);

                string fileName = await Product.Photo.SaveImageAsync(_env.WebRootPath, folder);
                dbProduct.Image = fileName;
            }
            dbProduct.Name = Product.Name;
            dbProduct.Image = Product.Image;
            dbProduct.Price = Product.Price;
            dbProduct.Count = Product.Count;
            //dbProduct.Category.Id = (int)CategoryId;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region ProductDelete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Product Product = await _context.Products.FindAsync(id);
            if (Product == null) return NotFound();
            return View(Product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null) return NotFound();
            Product Product = await _context.Products.FindAsync(id);
            if (Product == null) return NotFound();
            string folder = Path.Combine("img");
            //Helper.DeletePhoto(_env.WebRootPath, folder, Product.Image);


            _context.Products.Remove(Product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion


    }
}
