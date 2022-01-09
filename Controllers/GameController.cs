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
    }
}