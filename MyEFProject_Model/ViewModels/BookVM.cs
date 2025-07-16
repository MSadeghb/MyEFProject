using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEFProject_Model.Models;

namespace MyEFProject_Model.ViewModels
{
   public class BookVM
    {
        public Book Book { get; set; }
        public IEnumerable<SelectListItem> PublisherList { get; set; }
        public IEnumerable<SelectListItem> CategorieList { get; set; }
    }
}
