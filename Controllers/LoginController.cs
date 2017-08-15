using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using weddingplanner.Models;
using System.Linq;

namespace weddingplanner.Controllers
{
    public class LoginController : Controller
    {
        private WeddingContext _context;

        public LoginController(WeddingContext context)
        {
            _context = context;
        }

        // GET: /
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User ExistingUser = _context.Users.SingleOrDefault(user => user.Email == model.Email);
                if (ExistingUser != null)
                {
                    ViewBag.Message = "User with this email already exists!";
                    return View("Index", model);
                }
                User NewUser = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.Add(NewUser);
                _context.SaveChanges();
                NewUser = _context.Users.SingleOrDefault(user => user.Email == NewUser.Email);
                HttpContext.Session.SetInt32("UserId", NewUser.UserId);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View("Index", model);
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string LogEmail, string LogPassword)
        {
            User FoundUser = _context.Users.SingleOrDefault(user => user.Email == LogEmail && user.Password == LogPassword);
            if (FoundUser == null)
            {
                ViewBag.Message = "Login failed.";
                return View("Index");
            }
            else
            {
                HttpContext.Session.SetInt32("UserId", FoundUser.UserId);
                return RedirectToAction("Index", "Dashboard");
            }
        }

        [HttpGet]
        [Route("Logoff")]
        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
    }
}
