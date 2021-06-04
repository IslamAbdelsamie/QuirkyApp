using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using QuirkyApp.Models;

namespace QuirkyApp.Models
{
    public class Books
    {
        public int Id { get; set; }

        [Required]
        public int ISBN { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MaxLength(20)]
        public string Author { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

        public string Image { get; set; }

        [Required]
        [Range(0, 1000)]
        public int Availiability { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        [Required]
        [Display(Name = "Publication Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:MM/dd/yyyy}", ApplyFormatInEditMode =true)]
        public DateTime PublicationDate  { get; set; }

        [Required]
        [Range(0, 1000)]
        public int Pages { get; set; }

        [Display(Name ="Genre")]
        public int GenresId { get; set; }
        public Genres Genres  { get; set; }
    }
}