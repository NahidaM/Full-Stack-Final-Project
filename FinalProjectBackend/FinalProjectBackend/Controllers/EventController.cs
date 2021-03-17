using FinalProjectBackend.DAL;
using FinalProjectBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _db;
        public EventController(AppDbContext db) 
        {
            _db = db;
        }
       
        [HttpGet]
        public IActionResult Index()
        {
            List<Event> Event = _db.Events.Where(i=>i.IsDeleted == false).Include(c=>c.CategoryEvent).ToList();
            return View(Event);
        }

        public IActionResult Detail(int? id)
        {
            Event Event = _db.Events.Include(c=>c.CategoryEvent).FirstOrDefault(e => e.Id == id); 
            return View(Event);
        }

    }
}
