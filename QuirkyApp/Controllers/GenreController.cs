using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using QuirkyApp.Models;

namespace QuirkyApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class GenreController : Controller
    {
        private ApplicationDbContext _Context;

        public GenreController()
        {
            _Context = new ApplicationDbContext();
        }


        // GET: Genre
        public ActionResult Index()
        {
            var GenreList = _Context.Genres.ToList();
            return View(GenreList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create (Genres genres)
        {
            if (!ModelState.IsValid)
            {
                return View(genres);
            }
            else
            {
                genres.GenreSlug = genres.GenreName.Replace(" ", "").ToLower();
                if (_Context.Genres.Any(G => G.GenreSlug == genres.GenreSlug))
                {
                    ModelState.AddModelError("", "This Genre already exist");
                    return View(genres);
                }
                else
                {
                    _Context.Genres.Add(genres);
                    _Context.SaveChanges();
                    TempData["SM"] = "The Genre has been added Successfully";
                    return RedirectToAction("Index", "Genre");
                }
               
            }
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var Genre = _Context.Genres.SingleOrDefault(G => G.Id == Id);
            if (Genre == null)
            {
                return Content("This Genre does not exist");
            }
            else
            {
                return View(Genre);
            }

        }

        [HttpPost]
        public ActionResult Edit(Genres genres)
        {
            if (!ModelState.IsValid)
            {
                return View(genres);
            }
            else
            {
                genres.GenreSlug = genres.GenreName.Replace(" ", "").ToLower();
                if (_Context.Genres.Where(G => G.Id != genres.Id).Any(G => G.GenreSlug == genres.GenreSlug))
                {
                    ModelState.AddModelError("", "This genre already exist");
                    return View(genres);
                }
                else
                {
                    var GenreInDb = _Context.Genres.SingleOrDefault(G => G.Id == genres.Id);
                    GenreInDb.GenreName = genres.GenreName;
                    GenreInDb.GenreSlug = genres.GenreSlug;
                    _Context.SaveChanges();
                    TempData["SM"] = "The genre has been edited successfully";
                    return RedirectToAction("Index", "Genre");
                }
                
            }
        }

        public ActionResult Delete(int Id)
        {
            var Genre = _Context.Genres.SingleOrDefault(G => G.Id == Id);
            if (Genre == null)
            {
                return Content("This Genre Does Not Exist");

            }
            else
            {
                _Context.Genres.Remove(Genre);
                _Context.SaveChanges();
                TempData["SM"] = "The Genre has been deleted Successfully";
                return RedirectToAction("Index", "Genre");

            }
        }
    }
}