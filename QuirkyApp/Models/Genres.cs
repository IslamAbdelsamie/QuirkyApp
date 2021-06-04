using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QuirkyApp.Models
{
    public class Genres
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Genre Name")]
        [MaxLength(25)]
        public string GenreName  { get; set; }

        [MaxLength(25)]
        public string GenreSlug  { get; set; }
    }
}