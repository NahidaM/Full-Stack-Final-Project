using FinalProjectBackend.DAL;
using FinalProjectBackend.Models;
using FinalProjectBackend.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM 
            {
                Sliders = _db.Sliders.ToList(),
                AboutSlider = _db.AboutSliders.FirstOrDefault(),
                Volunteers= _db.Volunteers.ToList(),
                Workshop= _db.Workshops.FirstOrDefault(), 
                Events=_db.Events.ToList(),
                OurPurpose=_db.OurPurposes.FirstOrDefault(),  
                Services=_db.Services.ToList()

            };

            return View(homeVM);
        }

        public IActionResult Search(string search)
        {
            IEnumerable<Event> model = _db.Events.Where(p => p.Title.Contains(search)).OrderByDescending(p => p.Id).Take(8);
            return PartialView("_SearchPartial", model);
        }


    }
}
