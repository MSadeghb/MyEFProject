using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MyEFProject_DataAccess.Data;
using MyEFProject_Model.Models;

namespace MyEFProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Category> list = _context.Categories.ToList();
            return View(list);
        }

        public IActionResult Upsert(int? id)
        {
            Category category=new Category();
            if (id == null)
                return View(category);

            category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Upsert(Category category)
        {
            if (category.Id == 0)
                _context.Categories.Add(category);
            else
            {
                _context.Categories.Update(category);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var category = _context.Categories.First(c => c.Id == id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult AddMultipleRecords()
        {
            for (int i = 0; i < 9; i++)
            {
                _context.Categories.Add(new Category()
                {
                    Name = "Category "+ i
                });
                
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult AddMultipleRecords2()
        {
            List<Category> list = new List<Category>();
            for (int i = 20; i < 30; i++)
            {
               list.Add(new Category(){Name = $"Cat {i}"});
            }
            _context.Categories.AddRange(list);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult RemoveRange()
        {
            List<Category> list = _context.Categories.OrderByDescending(c => c.Id).Take(5).ToList();
            _context.Categories.RemoveRange(list);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
