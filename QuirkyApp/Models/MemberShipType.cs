using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QuirkyApp.Models
{
    public class MemberShipType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Sign Up Fees")]
        [Range(0,100)]
        [DataType(DataType.Currency)]
        public byte SignUpFee   { get; set; }

        [Required]
        [Display(Name = "Charge Rate On One Month")]
        [Range(0, 100)]
        public byte ChargeRateOnOneMonth   { get; set; }

        [Required]
        [Display(Name = "Charge Rate On Six Month")]
        [Range(0, 100)]
        public byte ChargeRateOnSixMonth  { get; set; }
    }
}