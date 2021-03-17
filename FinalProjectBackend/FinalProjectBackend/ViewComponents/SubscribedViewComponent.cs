using FinalProjectBackend.DAL;
using FinalProjectBackend.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.ViewComponents
{
    public class SubscribedViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;
        public SubscribedViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            SubscribeVM subscribe = new SubscribeVM()
            {
              Subscribe = _db.Subscribes.FirstOrDefault() 
            };
            return View(await Task.FromResult(subscribe));
        }
    }
    
}
