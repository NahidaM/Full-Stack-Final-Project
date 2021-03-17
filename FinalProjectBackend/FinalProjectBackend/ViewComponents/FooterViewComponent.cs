using FinalProjectBackend.DAL;
using FinalProjectBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.ViewComponents
{
    public class FooterViewComponent: ViewComponent
    {
        private readonly AppDbContext _db;
        public FooterViewComponent(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Bio model = _db.Bios.FirstOrDefault();
            return View(await Task.FromResult(model));
        }
    }
}
