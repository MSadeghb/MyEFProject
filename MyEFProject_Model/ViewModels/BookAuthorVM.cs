using Microsoft.AspNetCore.Mvc.Rendering;
using MyEFProject_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEFProject_Model.ViewModels
{
   public class BookAuthorVM
    {
        public BookAuthor BookAuthor { get; set; }
        public Book Book { get; set; }
        public IEnumerable <BookAuthor>BookAuthors { get; set; }
        public IEnumerable<SelectListItem>AuthorList  { get; set; }

    }
}
