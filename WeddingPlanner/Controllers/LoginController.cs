using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WeddingPlanner.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Controllers
{
    public class LoginController : Controller
    {
        private WeddingContext _context;
 
        public LoginController(WeddingContext context)
        {
            _context = context;
        }
        // GET:
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Login()
        {
            ViewBag.Errors = new List<string>();
            return View();
        }

        [HttpPost]
        [Route("/register")]
        public IActionResult Register(RegisterViewModel User)
        {
            if(ModelState.IsValid)
            {
                User NewPerson = new User
                {
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    Email = User.Email,
                    Password = User.Password,
                    Created_at =DateTime.Now,
                    Updated_at = DateTime.Now,
                };
                
                List<Guest> Guests = _context.Guests
                             .Include(user => user.User)
                              .ToList();
                _context.Users.Add(NewPerson);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("UserId", NewPerson.UserId);
                ViewBag.Weddings = new List<WeddingCreator>();
                return RedirectToAction("AllWeddings","AllWeddings");
            }
            else
            {
                ViewBag.Errors = ModelState.Values;
                return View("Login");
            }
        }

        [HttpPost]
        [Route("loginprocess")]
        public IActionResult Loginprocess(string Email, string Password)
        {
            List<User> ReturnedUserEmail = _context.Users.Where(user => user.Email == Email).ToList();
            if(ReturnedUserEmail.Count > 0)
            {
                if(ReturnedUserEmail[0].Password == Password)
                {
                    HttpContext.Session.SetInt32("UserId", ReturnedUserEmail[0].UserId);
                    ViewBag.Weddings = new List<WeddingCreator>();
                    return RedirectToAction("AllWeddings", "AllWeddings");
                }
                else
                {
                    ViewBag.Errors = new List<string>();
                    ViewBag.PasswordErrors = "Password is invalid!";
                    return View("Login");
                }
            }
            else
            {
                ViewBag.Errors = new List<string>();
                ViewBag.EmailErrors = "Email is invalid or not found!";
                return View("Login");
            }
        }
            
        }

 }