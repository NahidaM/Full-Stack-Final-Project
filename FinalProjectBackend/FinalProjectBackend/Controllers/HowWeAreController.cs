using FinalProjectBackend.DAL;
using FinalProjectBackend.Models;
using FinalProjectBackend.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.Controllers
{
    public class HowWeAreController : Controller
    {
        private readonly AppDbContext _db;
        public HowWeAreController(AppDbContext db)
        {
            _db = db; 
        }

        public IActionResult Index()
        {
            HowWeAreVM howWeAreVM = new HowWeAreVM
            {
                Cover = _db.Covers.ToList(),
                HowWeAre= _db.HowWeAres.FirstOrDefault(), 
                PurposeAssoc = _db.PurposeAssocs.FirstOrDefault()
            };
            return View(howWeAreVM);
        }

        public IActionResult Detail()
        {
            return View(); 
        }
    }
}
