using FinalProjectBackend.DAL;
using FinalProjectBackend.Models;
using FinalProjectBackend.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.Controllers
{
    public class MainController : Controller
    {
        private readonly AppDbContext _context;
        public MainController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult LoadMore(int skip)
        {
            List<Product> model = _context.Products.Include(p => p.Category).Where(p => p.Deleted == false && p.Count > 0).Skip(skip).Take(8).ToList();
            return PartialView("_ProductPartial", model);
        }
        #region Search
        //public IActionResult Search(string search)
        //{
        //    IEnumerable<Product> model = _context.Products.Where(p => p.Deleted == false).Where(pro => pro.Name.Contains(search)).OrderByDescending(p => p.Id);
        //    return PartialView("_SearchPartial", model);
        //}
        #endregion 

        public async Task<IActionResult> AddBasket(int id)
        {
            int basketCount = 0;
            Product isProduct = await _context.Products.FindAsync(id);
            if (isProduct == null)
            {
                return NotFound();
            }
            List<BasketVM> basket;
            if (Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }
            BasketVM isBasket = basket.FirstOrDefault(p => p.Id == id);
            if (isBasket == null)
            {
                basket.Add(new BasketVM
                {
                    Id = id,
                    Count = 1
                });
            }
            else
            {
                isBasket.Count += 1;
            }

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
            basketCount = basket.Sum(p => p.Count);
            return Content(basketCount.ToString());
        }
        public async Task<IActionResult> Basket()
        {
            ViewBag.Total = 0;
            List<BasketVM> dbBasket = new List<BasketVM>();
            if (Request.Cookies["basket"] != null)
            {
                List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
                foreach (BasketVM pro in basket)
                {
                    Product product = await _context.Products.FindAsync(pro.Id);
                    pro.Name = product.Name;
                    pro.Image = product.Image;
                    pro.Price = product.Price * pro.Count;
                    ViewBag.Total += pro.Price;
                    dbBasket.Add(pro);
                }
            }

            return View(dbBasket);
        }
        public IActionResult DeleteProduct(int id)
        {
            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            BasketVM delPro = basket.FirstOrDefault(p => p.Id == id);
            basket.Remove(delPro);
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
            return RedirectToAction(nameof(Basket));
        }
        public async Task<IActionResult> Add(int id)
        {
            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            BasketVM delPro = basket.FirstOrDefault(p => p.Id == id);
            Product product = await _context.Products.FindAsync(id);
            if (delPro.Count < product.Count)
            {
                delPro.Count += 1;
            }
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
            return RedirectToAction(nameof(Basket));
        }
        public IActionResult Remove(int id)
        {
            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            BasketVM delPro = basket.FirstOrDefault(p => p.Id == id);
            if (delPro.Count > 0)
            {
                delPro.Count -= 1;
            }
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
            return RedirectToAction(nameof(Basket));
        }
    }
}
