using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuirkyApp.Models;

namespace QuirkyApp.ViewModels
{
    public class BooksGenreViewModel
    {
        public Books Books  { get; set; }
        public IEnumerable<Genres> Genres  { get; set; }
    }
}