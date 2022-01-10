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
    }
}