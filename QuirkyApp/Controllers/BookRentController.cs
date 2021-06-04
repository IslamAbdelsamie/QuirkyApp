using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuirkyApp.ViewModels;
using QuirkyApp.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace QuirkyApp.Controllers
{
    public class BookRentController : Controller
    {
        private ApplicationDbContext _context;

        public BookRentController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminRentStatus()
        {
            var BookLists = _context.BookRents.Include(U => U.ApplicationUser).Include(B => B.Books).Include(R => R.RentType).Include(S => S.Status).ToList();

            return View(BookLists);
        }

        public ActionResult UserRentStatus()
        {
            var UserId = User.Identity.GetUserId();
            var BookLists = _context.BookRents.Where(B => B.ApplicationUserId == UserId).Include(B => B.Books).Include(R => R.RentType).Include(S => S.Status).ToList();

            return View(BookLists);
        }





        [HttpGet]
        public ActionResult BookRequest(int Id)
        {
            Session["BookId"] = Id;
            var Book = _context.Books.SingleOrDefault(B => B.Id == Id);
            var UserId = User.Identity.GetUserId();
            var MemberShipId = _context.Users.SingleOrDefault(U => U.Id == UserId);
            var memberShip = _context.MemberShipType.SingleOrDefault(M => M.Id == MemberShipId.MemberShipTypeId);

            ViewBag.OneMonthRent = (Book.Price * memberShip.ChargeRateOnOneMonth) / 100;
            ViewBag.SixMonthRent = (Book.Price * memberShip.ChargeRateOnSixMonth) / 100;

            var ViewModel = new BookRentViewModel
            {
                Books = _context.Books.Include(G => G.Genres).SingleOrDefault(B => B.Id == Id),
                RentType = _context.RentType.ToList()
            };

            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult BookRequest(BookRent bookRent)
        {
            bookRent.BooksId = (int)Session["BookId"];
            var Book = _context.Books.SingleOrDefault(B => B.Id == bookRent.BooksId);
            var UserId = User.Identity.GetUserId();
            if (_context.BookRents.Any(B => B.BooksId == Book.Id && B.ApplicationUserId == UserId && (B.StatusId == 1 || B.StatusId == 2 || B.StatusId == 4)))
            {
                ModelState.AddModelError("", "You Rented this book before");
                var viewModel = new BookRentViewModel
                {
                    Books = _context.Books.Include(G => G.Genres).SingleOrDefault(B => B.Id == Book.Id),
                    BookRent = bookRent,
                    RentType = _context.RentType.ToList()
                };
                return View(viewModel);

            }
            else
            {
                var MemberShipId = _context.Users.SingleOrDefault(U => U.Id == UserId);
                var memberShip = _context.MemberShipType.SingleOrDefault(M => M.Id == MemberShipId.MemberShipTypeId);
                bookRent.ApplicationUserId = UserId;
                bookRent.StatusId = 1;

                if (bookRent.RentTypeId == 1)
                {
                    bookRent.RentPrice = (Book.Price * memberShip.ChargeRateOnOneMonth) / 100;
                }
                else if (bookRent.RentTypeId == 2)
                {
                    bookRent.RentPrice = (Book.Price * memberShip.ChargeRateOnSixMonth) / 100;
                }

                _context.BookRents.Add(bookRent);
                Book.Availiability = Book.Availiability - 1;
                _context.SaveChanges();
                TempData["SM"] = "The rent request has been proceed successfully";
                return RedirectToAction("UserRentStatus", "BookRent");
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Approve(int Id)
        {
            var BookRent = _context.BookRents.Include(U => U.ApplicationUser).Include(B => B.Books).Include(R => R.RentType).Include(S => S.Status).Include(G => G.Books.Genres).SingleOrDefault(B => B.Id == Id);
            return View(BookRent);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Approve(BookRent bookRent)
        {
            var BookRentInDb = _context.BookRents.SingleOrDefault(B => B.Id == bookRent.Id);
            BookRentInDb.StatusId = 2;
            _context.SaveChanges();
            TempData["SM"] = "You Approved on this request";
            return RedirectToAction("AdminRentStatus", "BookRent");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Reject(int Id)
        {
            var BookRent = _context.BookRents.Include(U => U.ApplicationUser).Include(B => B.Books).Include(R => R.RentType).Include(S => S.Status).Include(G => G.Books.Genres).SingleOrDefault(B => B.Id == Id);
            return View(BookRent);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Reject(BookRent bookRent)
        {
            var BookRentInDb = _context.BookRents.SingleOrDefault(B => B.Id == bookRent.Id);
            BookRentInDb.StatusId = 3;
            var Book = _context.Books.SingleOrDefault(B => B.Id == BookRentInDb.BooksId);
            Book.Availiability += 1;
            _context.SaveChanges();
            TempData["SM"] = "You Rejected this request";
            return RedirectToAction("AdminRentStatus", "BookRent");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult PickUp(int Id)
        {
            var BookRent = _context.BookRents.Include(U => U.ApplicationUser).Include(B => B.Books).Include(R => R.RentType).Include(S => S.Status).Include(G => G.Books.Genres).SingleOrDefault(B => B.Id == Id);
            return View(BookRent);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult PickUp(BookRent bookRent)
        {
            var BookRentInDb = _context.BookRents.SingleOrDefault(B => B.Id == bookRent.Id);
            BookRentInDb.StatusId = 4;
            BookRentInDb.StartDate = DateTime.Now;
            if (BookRentInDb.RentTypeId == 1)
            {
                BookRentInDb.ScheduledEndDate = DateTime.Now.AddMonths(1);
            }
            else if (BookRentInDb.RentTypeId == 2)
            {
                BookRentInDb.ScheduledEndDate = DateTime.Now.AddMonths(6);
            }
            _context.SaveChanges();
            TempData["SM"] = "The book has been rented";
            return RedirectToAction("AdminRentStatus", "BookRent");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Close(int Id)
        {
            var BookRent = _context.BookRents.Include(U => U.ApplicationUser).Include(B => B.Books).Include(R => R.RentType).Include(S => S.Status).Include(G => G.Books.Genres).SingleOrDefault(B => B.Id == Id);
            return View(BookRent);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Close(BookRent bookRent)
        {
            var BookRentInDb = _context.BookRents.SingleOrDefault(B => B.Id == bookRent.Id);
            BookRentInDb.StatusId = 5;
            BookRentInDb.ActualEndDate = DateTime.Now;
            BookRentInDb.AdditionalCharge = bookRent.AdditionalCharge;
            var Book = _context.Books.SingleOrDefault(B => B.Id == BookRentInDb.BooksId);
            Book.Availiability += 1;
            _context.SaveChanges();
            TempData["SM"] = "This Request has been closed";
            return RedirectToAction("AdminRentStatus", "BookRent");
        }

        public ActionResult Delete(int Id)
        {
            var BookRent = _context.BookRents.SingleOrDefault(B => B.Id == Id);
            _context.BookRents.Remove(BookRent);
            _context.SaveChanges();
            TempData["SM"] = "The Request has been deleted successfully";
            return RedirectToAction("UserRentStatus", "BookRent");
        }
    }
}