using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuirkyApp.Models;

namespace QuirkyApp.ViewModels
{
    public class BookRentViewModel
    {
        public Books Books  { get; set; }
        public MemberShipType MemberShipType  { get; set; }
        public IEnumerable<RentType> RentType  { get; set; }
        public BookRent BookRent  { get; set; }
    }
}