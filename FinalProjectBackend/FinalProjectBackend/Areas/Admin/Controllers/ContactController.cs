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
    public class ContactController : Controller 
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public ContactController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        #region Index
        public IActionResult Index()
        {
            List<Contact> contact = _db.Contacts.ToList();
            return View(contact);
        }
        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Contact contact = _db.Contacts.Find(id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Contact contact)
        {
            if (id == null) return NotFound();
            Contact dbContact = await _db.Contacts.FindAsync(id);
            if (dbContact == null) return NotFound();

            if (contact.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }

                if (!contact.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                    return View();
                }

                if (contact.Photo.MaxSize(2000))
                {
                    ModelState.AddModelError("Photo", "Shekilin olchusu max 2mg ola biler");
                    return View();
                }

                string path = Path.Combine("img");
                Helper.DeletePhoto(_env.WebRootPath, path, dbContact.Image);
                string fileName = await contact.Photo.SaveImageAsync(_env.WebRootPath, path);
                dbContact.Image = fileName;
            }
            dbContact.TitleHeader = contact.TitleHeader;
            dbContact.Info = contact.Info;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
