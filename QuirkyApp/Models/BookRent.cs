using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuirkyApp.Models;
using System.ComponentModel.DataAnnotations;

namespace QuirkyApp.Models
{
    public class BookRent
    {
        public int Id { get; set; }

        [Display(Name ="Start Date")]
        public DateTime? StartDate  { get; set; }

        [Display(Name = "Actual End Date")]
        public DateTime? ActualEndDate  { get; set; }

        [Display(Name = "Scheduled End Date")]
        public DateTime? ScheduledEndDate  { get; set; }

        [Display(Name = "Rent Price")]
        public double RentPrice  { get; set; }

        [Display(Name = "Additional Charge")]
        public double? AdditionalCharge { get; set; }

        public int BooksId { get; set; }
        public Books Books  { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser  { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }
        public Status Status  { get; set; }

        [Display(Name ="Type Of Rent")]
        public int RentTypeId { get; set; }
        public RentType RentType  { get; set; }
    }
}