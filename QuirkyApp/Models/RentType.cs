using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QuirkyApp.Models
{
    public class RentType
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string RentName  { get; set; }
    }
}