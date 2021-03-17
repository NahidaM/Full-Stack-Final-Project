using FinalProjectBackend.DAL;
using FinalProjectBackend.Extentions;
using FinalProjectBackend.Helpers;
using FinalProjectBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FinalProjectBackend.Areas.Admin.Controllers
{
    [Area("Admin")] 
    [Authorize(Roles = "Admin")]
    public class EventController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public EventController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        // GET: EventController
             #region Index
        public ActionResult Index()
        {
            List<Event> Events = _db.Events.Where(p => p.IsDeleted == false).ToList();
            return View(Events);
        }
        #endregion 

            #region EventDetail
            public async Task<ActionResult> Detail(int? id)
            {
                if (id == null)
                {
                    NotFound();
                }
                Event Event = await _db.Events
                         .Include(p => p.CategoryEvent)
                            .Where(p => p.IsDeleted == false && p.Id == id).FirstOrDefaultAsync();
                return View(Event);
            }
            #endregion

            #region EventCreate
            public IActionResult Create()
            {
                ViewBag.Events = _db.CategoryEvents.Where(b => b.IsDeleted == false).ToList();
                return View();
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Event Event, int? CategoryEventId)
            {
                ViewBag.Events = _db.CategoryEvents.Where(b => b.IsDeleted == false).ToList();
                Event newEvent = new Event();
                if (Event.Photo == null)
                {
                    ModelState.AddModelError("Photo", "Shekil bolmesi bosh qala bilmez!");
                    return View();
                }

                if (!Event.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                    return View();
                }

                if (!Event.Photo.MaxSize(600))
                {
                    ModelState.AddModelError("Photo", "Sheklin maksimum olcusu 200 kb ola biler");
                    return View();
                }

                string folder = Path.Combine("img");
                string fileName = await Event.Photo.SaveImageAsync(_env.WebRootPath, folder);

                if (CategoryEventId == null)
                {
                    ModelState.AddModelError("CategoryEventId", "Xahis edirik CategoryEventId secin!");
                    ViewBag.SelectError = "Please select author";
                    return View();
                }

                newEvent.Image = fileName;
                newEvent.Description = Event.Description;
                newEvent.Title = Event.Title;
                newEvent.Time = Event.Time;
                newEvent.CategoryEventId =(int)CategoryEventId; 
                await _db.Events.AddAsync(newEvent);
                await _db.SaveChangesAsync();

                #region SubscribedEmail
            List<SubscribedEmail> emails = _db.SubscribedEmails.Where(e => e.HasDeleted == false).ToList();
            foreach (SubscribedEmail email in emails)
            {
                SendEmail(email.Email, "Yeni bir event yaradildi.", "<h1>Yeni bir event yaradildi</h1>");
            }
            #endregion

            return RedirectToAction(nameof(Index));
            }
            #endregion

            #region EventUpdate
            public async Task<IActionResult> Update(int? id) 
            {
                if (id == null) return NotFound();
                ViewBag.Events = _db.CategoryEvents.Where(b => b.IsDeleted == false).ToList();
                Event Event = await _db.Events.Where(p => p.IsDeleted == false).FirstOrDefaultAsync(p => p.Id == id);
                if (Event == null) return NotFound();
                ViewBag.EventsId = Event.CategoryEvent.Id;
                return View(Event);
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Update(int? id, Event Event, int? CategoryEventId)
            {
                ViewBag.Events = _db.CategoryEvents.Where(b => b.IsDeleted == false).ToList();
                Event newEvent = new Event();
                Event dbEvent = await _db.Events.Where(p => p.IsDeleted == false)
                   .Include(p => p.CategoryEvent).FirstOrDefaultAsync(p => p.Id == id);
                if (dbEvent == null) return NotFound();
                ViewBag.CategoryEventId = dbEvent.CategoryEvent.Id;

                if (Event.Photo != null)
                {
                    if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    {
                        return View();
                    }
                    if (!Event.Photo.IsImage())
                    {
                        ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                        return View();
                    }

                    if (!Event.Photo.MaxSize(600))
                    {
                        ModelState.AddModelError("Photo", "Sheklin maksimum olcusu 200 kb ola biler");
                        return View();
                    }

                    string folder = Path.Combine("img");  
                    Helper.DeletePhoto(_env.WebRootPath, folder, dbEvent.Image);

                    string fileName = await Event.Photo.SaveImageAsync(_env.WebRootPath, folder);
                    dbEvent.Image = fileName;
                }
                      dbEvent.Description = Event.Description;
                      dbEvent.Title = Event.Title;
                      dbEvent.Time = Event.Time;
                      //dbEvent.CategoryEvent.Id = (int)CategoryEventId;
                     await _db.SaveChangesAsync();
                     return RedirectToAction(nameof(Index));
            }
            #endregion

            #region EventDelete
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null) return NotFound();
                Event Event = await _db.Events.FindAsync(id);
                if (Event == null) return NotFound();
                return View(Event);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            [ActionName("Delete")]
            public async Task<IActionResult> DeleteEvent(int? id)
            {
                if (id == null) return NotFound();
                Event Event = await _db.Events.FindAsync(id);
                if (Event == null) return NotFound();
                string folder = Path.Combine("img");
                Helper.DeletePhoto(_env.WebRootPath, folder, Event.Image);


                _db.Events.Remove(Event);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        #endregion 

            #region SendEmail
        public void SendEmail(string email, string subject, string htmlMessage)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,

                Credentials = new NetworkCredential()
                {
                    UserName = "nahidanm22@gmail.com",
                    Password = "nahida1999"
                }
            };
            MailAddress fromEmail = new MailAddress("nahidanm22@gmail.com", "Nahida");
            MailAddress toEmail = new MailAddress(email, "Nahida");
            MailMessage message = new MailMessage()
            {
                From = fromEmail,
                Subject = subject,
                Body = htmlMessage
            };
            message.To.Add(toEmail);
            client.Send(message);
        }
        #endregion

    }




}
