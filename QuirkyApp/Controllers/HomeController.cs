using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using QuirkyApp.Models;

namespace QuirkyApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            var BooksList = _context.Books.ToList();
            return View(BooksList);
        }

        public ActionResult About()
        {
            return View();
        }

       
    }
}