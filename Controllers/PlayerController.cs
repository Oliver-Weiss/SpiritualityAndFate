using SpiritualityAndFate.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace SpiritualityAndFate.Controllers
{
    public class PlayerController : Controller
    {
        private SAFContext _context;

        public PlayerController(SAFContext context)
        {
            _context = context;
        }

        // LOGIN & REGISTRATION
        [HttpGet("")]
        public IActionResult LogReg()
        {
            return View();
        }

        [HttpPost("registration")]
        public IActionResult Registration(Player newPlayer)
        {
            if(_context.Players.Any(p => p.Username == newPlayer.Username))
            {
                ModelState.AddModelError("Username", "Username already in use!");
            }
            
            if(ModelState.IsValid)
            {
                _context.Players.Add(newPlayer);
                PasswordHasher<Player> Hasher = new PasswordHasher<Player>();
                newPlayer.Password = Hasher.HashPassword(newPlayer, newPlayer.Password);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("loggedInPlayer", newPlayer.PlayerId);
                return Redirect("/name");
            }
            else
            {
                return View("LogReg");
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUser playerSubmission)
        {
            if(ModelState.IsValid)
            {
                    var playerInDb = _context.Players.FirstOrDefault(p => p.Username == playerSubmission.LogUsername);

                    if (playerInDb == null)
                    {
                        ModelState.AddModelError("LogUsername", "Invalid username!");
                        return View("LogReg");
                    }

                    var hasher = new PasswordHasher<LoginUser>();
                    var result = hasher.VerifyHashedPassword(playerSubmission, playerInDb.Password, playerSubmission.LogPassword);

                    if(result == 0)
                    {
                        ModelState.AddModelError("LogPassword", "Invalid password!");
                        return View("LogReg");
                    }

                    HttpContext.Session.SetInt32("loggedInPlayer", playerInDb.PlayerId);
                    return Redirect("/name");
            }
            else
            {
                return View("LogReg");
            }
        }

        // CHARACTER CREATION
        [HttpGet("name")]
        public IActionResult Starting()
        {
            if(HttpContext.Session.GetInt32("loggedInPlayer") == null)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost("naming")]
        public IActionResult Naming(string name)
        {
            if(name == null)
            {
                ModelState.AddModelError("Name", "Please input a name, my child.");
                return View("Starting");
            }
            Player RetrievedPlayer = _context.Players
                .FirstOrDefault(player => player.PlayerId == HttpContext.Session.GetInt32("loggedInPlayer"));

            RetrievedPlayer.Name = name;
            RetrievedPlayer.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return Redirect("/age");
        }

        [HttpGet("age")]
        public IActionResult Age()
        {
            if(HttpContext.Session.GetInt32("loggedInPlayer") == null)
            {
                return Redirect("/");
            }
            
            ViewBag.Player =  _context.Players
                .FirstOrDefault(player => player.PlayerId == HttpContext.Session.GetInt32("loggedInPlayer"));
            return View();
        }

        [HttpPost("timeoflife")]
        public IActionResult TimeOfLife(string agerange)
        {
            Player RetrievedPlayer = _context.Players
                .FirstOrDefault(player => player.PlayerId == HttpContext.Session.GetInt32("loggedInPlayer"));

            RetrievedPlayer.AgeRange= agerange;
            RetrievedPlayer.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return Redirect("/species");
        }

        [HttpGet("species")]
        public IActionResult Species(string species)
        {
            if(HttpContext.Session.GetInt32("loggedInPlayer") == null)
            {
                return Redirect("/");
            }
            
            ViewBag.Player =  _context.Players
                .FirstOrDefault(player => player.PlayerId == HttpContext.Session.GetInt32("loggedInPlayer"));
            return View();
        }

        [HttpPost("transform")]
        public IActionResult Transform(string species)
        {
            Player RetrievedPlayer = _context.Players
                .FirstOrDefault(player => player.PlayerId == HttpContext.Session.GetInt32("loggedInPlayer"));

            RetrievedPlayer.Species= species;
            RetrievedPlayer.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return Redirect("/leave");
        }

        [HttpGet("leave")]
        public IActionResult Leave()
        {
            if(HttpContext.Session.GetInt32("loggedInPlayer") == null)
            {
                return Redirect("/");
            }
             Player RetrievedPlayer = _context.Players
                .FirstOrDefault(player => player.PlayerId == HttpContext.Session.GetInt32("loggedInPlayer"));

            ViewBag.Player =  RetrievedPlayer;
            return View();
        }
    }
}