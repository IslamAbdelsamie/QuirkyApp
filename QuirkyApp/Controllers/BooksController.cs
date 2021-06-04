using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using QuirkyApp.Models;
using QuirkyApp.ViewModels;
using System.IO;

namespace QuirkyApp.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext _context;

        public BooksController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Books
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            var BooksList = _context.Books.Include(G => G.Genres).ToList();
            return View(BooksList);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var ViewModel = new BooksGenreViewModel
            {
                Genres = _context.Genres.ToList()
            };
            return View(ViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Books books, HttpPostedFileBase ImageFile)
        {
            if (!ModelState.IsValid)
            {
                var ViewModel = new BooksGenreViewModel
                {
                    Books = books,
                    Genres = _context.Genres.ToList()
                };

                return View(ViewModel);
            }
            else
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string extension = Path.GetExtension(ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmsssfff") + extension;
                    books.Image = "~/Uploads/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                    ImageFile.SaveAs(fileName);
                }
                

                books.DateAdded = DateTime.Now;

                _context.Books.Add(books);
                _context.SaveChanges();
                TempData["SM"] = "The Book Has Been Added Successfully";

                return RedirectToAction("Index", "Books");
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int Id)
        {
            var Book = _context.Books.SingleOrDefault(B => B.Id == Id);
            if (Book == null)
            {
                return Content("This Book Does Not Exist");
            }
            else
            {
                var ViewModel = new BooksGenreViewModel
                {
                    Books = Book,
                    Genres = _context.Genres.ToList()
                };

                return View(ViewModel);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Books books, HttpPostedFileBase ImageFile)
        {
            if (!ModelState.IsValid)
            {
                var ViewModel = new BooksGenreViewModel
                {
                    Books = books,
                    Genres = _context.Genres.ToList()
                };

                return View(ViewModel);
            }
            else
            {
                var BookInDb = _context.Books.SingleOrDefault(B => B.Id == books.Id);
                BookInDb.ISBN = books.ISBN;
                BookInDb.Pages = books.Pages;
                BookInDb.Price = books.Price;
                BookInDb.PublicationDate = books.PublicationDate;
                BookInDb.Title = books.Title;
                BookInDb.Author = books.Author;
                BookInDb.Availiability = books.Availiability;
                BookInDb.Description = books.Description;
                BookInDb.GenresId = books.GenresId;
                BookInDb.DateAdded = DateTime.Now;

                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string extension = Path.GetExtension(ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmsssfff") + extension;
                    BookInDb.Image = "~/Uploads/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                    ImageFile.SaveAs(fileName);
                }

                _context.SaveChanges();
                TempData["SM"] = "The Book Has Been Edited Successfully";
                return RedirectToAction("Index", "Books");
            }

        }

        public ActionResult Details(int Id)
        {
            var Book = _context.Books.Include(G => G.Genres).SingleOrDefault(B => B.Id == Id);
            return View(Book);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            var book = _context.Books.SingleOrDefault(B => B.Id == Id);

            if (book == null)
            {
                return Content("This Book Does not exist");
            }
            else
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                TempData["SM"] = "The Book has been deleted successfully";

                return RedirectToAction("Index", "Books");
            }
        }

        public ActionResult BookShop()
        {
            var BooksList = _context.Books.Include(G => G.Genres).ToList();
            return View(BooksList);
        }

        [HttpGet]
        public ActionResult BookSearch()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BookSearch(string Search)
        {
            var BooksList = _context.Books.Where(B => B.Title.Contains(Search)).ToList();
            return View(BooksList);

        }

    }
}