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
    public class MemberShipController : Controller
    {
        private ApplicationDbContext _context;

        public MemberShipController()
        {
            _context = new ApplicationDbContext();
        }


        // GET: MemberShip
        public ActionResult Index()
        {
            var MemberShipTypesList = _context.MemberShipType.ToList();
            return View(MemberShipTypesList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MemberShipType memberShipType)
        {
            if (!ModelState.IsValid)
            {
                return View(memberShipType);
            }
            else
            {
                _context.MemberShipType.Add(memberShipType);
                
                _context.SaveChanges();
                TempData["SM"] = "The New MemberShip has been added successfully";
                return RedirectToAction("Index", "MemberShip");
            }
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var MemberShip = _context.MemberShipType.SingleOrDefault(M => M.Id == Id);

            if (MemberShip == null)
            {
                return Content("This MemberShip Does not exist");
            }
            else
            {
                return View(MemberShip);
            }
        }

        [HttpPost]
        public ActionResult Edit(MemberShipType memberShipType)
        {
            if (!ModelState.IsValid)
            {
                return View(memberShipType);
            }
            else
            {
                var MemberShipInDb = _context.MemberShipType.SingleOrDefault(M => M.Id == memberShipType.Id);
                MemberShipInDb.SignUpFee = memberShipType.SignUpFee;
                MemberShipInDb.ChargeRateOnOneMonth = memberShipType.ChargeRateOnOneMonth;
                memberShipType.ChargeRateOnSixMonth = memberShipType.ChargeRateOnSixMonth;

                _context.SaveChanges();
                TempData["SM"] = "The MemberShip Has been Edit Successfully";

                return RedirectToAction("Index", "MemberShip");
            }
        }

        public ActionResult Delete(int Id)
        {
            var MemberShip = _context.MemberShipType.SingleOrDefault(M => M.Id == Id);
            if (MemberShip == null)
            {
                return Content("This MemberShip Does not exist");
            }
            else
            {
                _context.MemberShipType.Remove(MemberShip);
                _context.SaveChanges();

                TempData["SM"] = "The MemberShip Has been Deleted Successfully";

                return RedirectToAction("Index", "MemberShip");
            }
        }
    }
}