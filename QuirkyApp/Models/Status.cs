using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QuirkyApp.Models
{
    public class Status
    {
        public int Id { get; set; }

        [MaxLength(10)]
        public string StatusName  { get; set; }
    }
}