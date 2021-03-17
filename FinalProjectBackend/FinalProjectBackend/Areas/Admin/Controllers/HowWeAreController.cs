using FinalProjectBackend.DAL;
using FinalProjectBackend.Extentions;
using FinalProjectBackend.Helpers;
using FinalProjectBackend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HowWeAreController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public HowWeAreController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        #region Index
        public IActionResult Index()
        {
            return View(_db.HowWeAres.ToList());
        }
        #endregion
        #region Update
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            HowWeAre howWeAre = _db.HowWeAres.FirstOrDefault(p => p.Id == id);
            if (howWeAre == null) return NotFound();
            return View(howWeAre);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, HowWeAre howWeAre)
        {
            if (!ModelState.IsValid) return View();
            if (id == null) return NotFound();
            HowWeAre dbhowWeAre = await _db.HowWeAres.FindAsync(id);
            if (dbhowWeAre == null) return NotFound();
            if (howWeAre.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }


                if (howWeAre.Photo.MaxSize(2000))
                {
                    ModelState.AddModelError("Photo", "Shekilin olchusu max 2mg ola biler");
                    return View();
                }
                string path = Path.Combine("img", "testimonial");
                //Helper.DeletePhoto(_env.WebRootPath, path, dbhowWeAre.Photo);
                string fileName = await howWeAre.Photo.SaveImageAsync(_env.WebRootPath, path);
                //dbhowWeAre.Photo = fileName;
            }

            dbhowWeAre.Title = howWeAre.Title;
            dbhowWeAre.Description = howWeAre.Description;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HowWeAre howWeAre)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!howWeAre.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                return View();
            }

            if (howWeAre.Photo.MaxSize(2000))
            {
                ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
                return View();
            }

            string path = Path.Combine("img", "testimonial");

            string fileName = await howWeAre.Photo.SaveImageAsync(_env.WebRootPath, path);
            HowWeAre newhowWeAre = new HowWeAre();
            newhowWeAre.Title = howWeAre.Title;
            newhowWeAre.Description = howWeAre.Description;
            //newhowWeAre.Photo = fileName; 
            
            await _db.HowWeAres.AddAsync(howWeAre);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            HowWeAre howWeAre = _db.HowWeAres.Find(id);
            if (id == null) return NotFound();
            return View(howWeAre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> WeAre(int? id) 
        {
            if (id == null) return NotFound();
            HowWeAre howWeAre = _db.HowWeAres.Find(id);
            if (howWeAre == null) return NotFound();
            string path = Path.Combine("www", "img");
            //Helper.DeletePhoto(_env.WebRootPath, path, howWeAre.Photo);
            _db.HowWeAres.Remove(howWeAre);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "HowWeAre");
        }
        #endregion

    }
}
