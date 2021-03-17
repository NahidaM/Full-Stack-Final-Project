using FinalProjectBackend.DAL;
using FinalProjectBackend.Models;
using FinalProjectBackend.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
   
    public class UsersController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public UsersController(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        #region Index
        public IActionResult Index(string name)
        {
            var users = (name == null) ? _userManager.Users.Where(x => x.IsActivated == true).ToList() :
                _userManager.Users.Where(x => x.IsActivated == true && x.FullName.ToLower().Contains(name.ToLower())).ToList();
            return View(users);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                UserRoleVM userRoleVM = new UserRoleVM
                {
                    User = user,
                    UserRoles = userRoles
                };
                return View(userRoleVM);
            }
            return NotFound();
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();
            AppUser newUser = new AppUser
            {
                FullName = register.FullName,
                Email = register.Email,
                UserName = register.UserName
            };

            IdentityResult identityResult = await _userManager.CreateAsync(newUser, register.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(register);
            }
            newUser.EmailConfirmed = true;
            newUser.IsActivated = true;

            await _userManager.AddToRoleAsync(newUser, "Member");
            TempData["success"] = "User Created";
            return RedirectToAction("Index", "Users");
        }
        #endregion

        #region IsActive
        public async Task<IActionResult> IsActivate(string id)
        {
            if (id == null) return NotFound();
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsActivate(string id, bool IsActivated)
        {
            if (id == null) return NotFound();
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            user.IsActivated = IsActivated;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region DeleteList
        public IActionResult DeleteList()
        {
            return View(_userManager.Users.Where(d => d.IsActivated == false));
        }
        #endregion
    }
}
