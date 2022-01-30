using SpiritualityAndFate.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TestGame.Controllers
{
    public class GameController : Controller
    {
        private SAFContext _context;

        public GameController(SAFContext context)
        {
            _context = context;
        }

        // HOME
        [HttpGet("homebase")]
        public IActionResult Cave()
        {
            if(HttpContext.Session.GetInt32("loggedInPlayer") == null)
            {
                return Redirect("/");
            }
            ViewBag.Player =  _context.Players
                .FirstOrDefault(player => player.PlayerId == HttpContext.Session.GetInt32("loggedInPlayer"));

            ViewBag.LoggedInPlayer = _context.Players
                .FirstOrDefault(p => p.PlayerId == (int)HttpContext.Session.GetInt32("loggedInPlayer"));
            return View();
        }

        [HttpGet("inventory")]
        public IActionResult Inventory()
        {
            if(HttpContext.Session.GetInt32("loggedInPlayer") == null)
            {
                return Redirect("/");
            }
            ViewBag.Player =  _context.Players
            .Include(i => i.Inventory)
            .FirstOrDefault(player => player.PlayerId == HttpContext.Session.GetInt32("loggedInPlayer"));
            return View();
        }

        [HttpGet("itemcreation")]
        public IActionResult ItemCreation()
        {
            if(HttpContext.Session.GetInt32("loggedInPlayer") == null)
            {
                return Redirect("/");
            }
            if (HttpContext.Session.GetInt32("loggedInPlayer") != 1)
            {
                return Redirect("/homebase");
            }
            return View();
        }

        [HttpPost("Game/playinggod")]
        public IActionResult NewItem(Item newItem)
        {
            if (ModelState.IsValid)
            {
                newItem.PlayerId = (int)HttpContext.Session.GetInt32("loggedInPlayer");
                _context.Items.Add(newItem);
                _context.SaveChanges();
                return RedirectToAction("Cave");
            }
            else
            {
                return View("ItemCreation");
            }
        }
        
        [HttpGet("{ItemId}")]
        public IActionResult ItemView(int ItemId)
        {
            if (HttpContext.Session.GetInt32("loggedInPlayer") == null)
            {
                return Redirect("/");
            }
            Item selected = _context.Items
                .Include(item => item.Owner)
                .FirstOrDefault(item => item.ItemId == ItemId);

            return View(selected);
        }

        [HttpGet("outside")]
        public IActionResult Outside()
        {
            if (HttpContext.Session.GetInt32("loggedInPlayer") == null)
            {
                return Redirect("/");
            }

            ViewBag.Inventory =  _context.Items
                .Include(i => i.Owner)
                .FirstOrDefault(item => item.PlayerId == HttpContext.Session.GetInt32("loggedInPlayer"));
            return View();
        }

        // GAMEPLAY SECTION
        [HttpGet("plains")]
        public IActionResult Plains()
        {
            if (HttpContext.Session.GetInt32("loggedInPlayer") == null)
            {
                return Redirect("/");
            }

            ViewBag.Player = _context.Players
                .Include(i => i.Inventory)
                .FirstOrDefault(player => player.PlayerId == HttpContext.Session.GetInt32("loggedInPlayer"));
            return View();
        }

        [HttpGet("givefood")]
        public IActionResult GiveFood()
        {
            Player RetrievedPlayer = _context.Players
                .Include(i => i.Inventory)
                .FirstOrDefault(player => player.PlayerId == HttpContext.Session.GetInt32("loggedInPlayer"));

            RetrievedPlayer.Inventory.Remove(_context.Items.FirstOrDefault(i => i.Title == "Raw Meat"));
            _context.SaveChanges();

            return Redirect("/dinner");
        }

        [HttpGet("dinner")]
        public IActionResult Dinner()
        {
            if (HttpContext.Session.GetInt32("loggedInPlayer") == null)
            {
                return Redirect("/");
            }

            ViewBag.Player = _context.Players
                .Include(i => i.Inventory)
                .FirstOrDefault(player => player.PlayerId == HttpContext.Session.GetInt32("loggedInPlayer"));
            return View();
        }

        [HttpGet("woods")]
         public IActionResult Woods()
        {
            if (HttpContext.Session.GetInt32("loggedInPlayer") == null)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpGet("usenecklace")]
        public IActionResult UseNecklace()
        {
            Player RetrievedPlayer = _context.Players
                .Include(i => i.Inventory)
                .FirstOrDefault(player => player.PlayerId == HttpContext.Session.GetInt32("loggedInPlayer"));

            RetrievedPlayer.Inventory.Remove(_context.Items.FirstOrDefault(i => i.Title == "Necklace"));
            _context.SaveChanges();

            return Redirect("/gratitude");
        }

        [HttpGet("gratitude")]
        public IActionResult Gratitude()
        {
            if (HttpContext.Session.GetInt32("loggedInPlayer") == null)
            {
                return Redirect("/");
            }

            ViewBag.Player = _context.Players
                .Include(i => i.Inventory)
                .FirstOrDefault(player => player.PlayerId == HttpContext.Session.GetInt32("loggedInPlayer"));
            return View();
        }

        [HttpGet("mountains")]
         public IActionResult Mountains()
        {
            if (HttpContext.Session.GetInt32("loggedInPlayer") == null)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpGet("applysalve")]
        public IActionResult ApplySalve()
        {
            Player RetrievedPlayer = _context.Players
                .Include(i => i.Inventory)
                .FirstOrDefault(player => player.PlayerId == HttpContext.Session.GetInt32("loggedInPlayer"));

            RetrievedPlayer.Inventory.Remove(_context.Items.FirstOrDefault(i => i.Title == "Healing Salve"));
            _context.SaveChanges();

            return Redirect("/helpinghands");
        }

        [HttpGet("helpinghands")]
        public IActionResult HelpingHands()
        {
            if (HttpContext.Session.GetInt32("loggedInPlayer") == null)
            {
                return Redirect("/");
            }

            ViewBag.Player = _context.Players
                .Include(i => i.Inventory)
                .FirstOrDefault(player => player.PlayerId == HttpContext.Session.GetInt32("loggedInPlayer"));
            return View();
        }
    }
}