using FinalProjectBackend.DAL;
using FinalProjectBackend.Extentions;
using FinalProjectBackend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private int _count;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context; 
            _env = env;
            _count = _context.Sliders.Count();
        }
        // GET: SliderController
        public ActionResult Index()
        {
            ViewBag.sliderCount = _count;
            return View(_context.Sliders.ToList());
        }

        // GET: SliderController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // GET: SliderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SliderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Slider slider)
        {
            if (slider.Photos == null)
            {
                return View();
            }
            string folder = Path.Combine("assets", "img");
            if ((_count + slider.Photos.Length) > 5)
            {
                ModelState.AddModelError("Photos", $"You can add {5 - _count} image!");
                return View();
            }
            foreach (IFormFile photo in slider.Photos)
            {
                if (!photo.IsImage())
                {
                    ModelState.AddModelError("Photos", $"{photo.FileName} - is not image file!");
                    return View();
                }
                if (!photo.MaxSize(200))
                {
                    ModelState.AddModelError("Photos", $"{photo.FileName} - image file size more than 200 kb");
                    return View();
                }
                Slider newSlider = new Slider();
                newSlider.Image = await photo.SaveImageAsync(_env.WebRootPath, folder);
                await _context.Sliders.AddAsync(newSlider);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: SliderController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: SliderController/Edit/slider
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Slider slider)
        {
            Slider img = await _context.Sliders.FindAsync(slider.Id);
            if (img == null)
            {
                return NotFound();
            }
            if (slider.Photo == null)
            {
                ModelState.AddModelError("Photo", "Please choose image file");
                return Content("Photo null");
            }
            if (!slider.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Please select only image file!");
                return Content("not image file");
            }
            if (slider.Photo.Length / 1024 > 200)
            {
                ModelState.AddModelError("Photo", "Please select an image file, which size less 200kb");
                return Content("photo size");
            }
            string fileName = Guid.NewGuid() + slider.Photo.FileName;
            string newFilePath = Path.Combine(_env.WebRootPath,  "img", fileName);
            string oldFilePath = Path.Combine(_env.WebRootPath,  "img", img.Image);
            using (FileStream file = new FileStream(newFilePath, FileMode.Create))
            {
                await slider.Photo.CopyToAsync(file);
            }
            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }
            img.Image = fileName;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: SliderController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            if (_count < 2)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: SliderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Delete))]
        public async Task<ActionResult> DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            if (_count < 2)
            {
                return NotFound();
            }
            string folder = Path.Combine("assets", "img");
            string filePath = Path.Combine(_env.WebRootPath, "assets", "img", slider.Image);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
