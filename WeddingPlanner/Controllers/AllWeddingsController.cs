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
    public class AllWeddingsController : Controller
    {
        private WeddingContext _context;
 
        public AllWeddingsController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("AllWeddings")]
        public IActionResult AllWeddings()
        {
            ViewBag.allweddings = new List<string>();
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.allweddings = _context.Weddings;
            List<WeddingCreator> Weddings = _context.Weddings
                .Include(x => x.Guests)
                .ThenInclude(x => x.User)
                .ToList();
            if(Weddings.Count < 1)
            {
                Weddings = new List<WeddingCreator>();
            }
                ViewBag.Weddings = Weddings;
                System.Console.WriteLine("We are in all weddings");
            return View("AllWeddings");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }

        [HttpGet]
        [Route("createwedding")]
        public IActionResult CreateWedding()
        {
            ViewBag.Errors = new List<string>();
            return View("CreateWedding");
        }

        [HttpPost]
        [Route("/savewedding")]
        public IActionResult SaveWedding(WeddingViewModel newWedding)
        {
            if(ModelState.IsValid)
            {
                WeddingCreator NewWedding = new WeddingCreator
                {
                    WeddingOne = newWedding.WeddingOne,
                    WeddingTwo = newWedding.WeddingTwo,
                    Date = newWedding.Date,
                    WeddingAddress = newWedding.WeddingAddress,
                    Created_at =DateTime.Now,
                    Updated_at = DateTime.Now,
                    UserId = (int)HttpContext.Session.GetInt32("UserId")
                };
                List<Guest> Guests = _context.Guests
                             .Include(user => user.User)
                             .Include(wedding => wedding.Wedding)
                             .ToList();
                _context.Weddings.Add(NewWedding);
                ViewBag.allweddings = new List<string>();
                _context.SaveChanges();
                return RedirectToAction("AllWeddings");
            }
            else
            {
                ViewBag.Errors = ModelState.Values;
                return View("CreateWedding");
            }
        }

        [HttpGet]
        [Route("/delete/{id}")]
        public IActionResult Delete(int id)
        {
            WeddingCreator RetrievedWedding = _context.Weddings.SingleOrDefault(wedding => wedding.WeddingId == id);
            List<Guest> RemoveGuests = _context.Guests.Where(guests => guests.WeddingId == id).ToList();
            foreach(var guest in RemoveGuests){
                _context.Guests.Remove(guest);
            }
            _context.Weddings.Remove(RetrievedWedding);
            _context.SaveChanges();
            ViewBag.allweddings = new List<string>();
            return RedirectToAction("AllWeddings");
        }

        [HttpGet]
        [Route("/onewedding/{id}")]
        public IActionResult OneWedding(int id)
        {
            WeddingCreator OneWedding = _context.Weddings.Where(wedding => wedding.WeddingId == id).Include(x => x.Guests).ThenInclude(x => x.User).SingleOrDefault();
            ViewBag.OneWedding = OneWedding;
            return View("OneWedding");
        }
        [HttpGet]
        [Route("/RSVP/{id}")]
        public IActionResult RSVP(int id)
        {
            Guest Guest = new Guest
            {
             UserId = (int)HttpContext.Session.GetInt32("UserId"),
             WeddingId = id
            };
            ViewBag.allweddings = new List<string>();
            ViewBag.Weddings = new List<string>();
            _context.Guests.Add(Guest);
            _context.SaveChanges();
            return RedirectToAction("AllWeddings");
        }
        [HttpGet]
        [Route("/UNRSVP/{id}")]
        public IActionResult UNRSVP(int id)
        {
            int UserId = (int)HttpContext.Session.GetInt32("UserId");
            Guest RemovedGuest =  _context.Guests.Where(x => x.UserId == UserId).Where(x => x.WeddingId == id).SingleOrDefault();
            _context.Guests.Remove(RemovedGuest);
            _context.SaveChanges();
            return RedirectToAction("AllWeddings");
        }        
    }
}