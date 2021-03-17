using FinalProjectBackend.DAL;
using FinalProjectBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.Controllers
{
    public class WeAreController : Controller
    {
        private readonly AppDbContext _db;
        public WeAreController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            WeAre weAre = new WeAre
            {

            };
            return View(weAre);
        }

        public IActionResult Detail()
        {
            return View();
        } 

    }
}
