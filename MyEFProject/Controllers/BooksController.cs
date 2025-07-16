using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyEFProject_DataAccess.Data;
using MyEFProject_Model.Models;
using MyEFProject_Model.ViewModels;

namespace MyEFProject.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext _db;

        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var books = _db.Books.Include(b=>b.Category).Include(b=>b.Publisher).Include(b=>b.BookDetail)
                .Include(b=>b.BookAuthors).ThenInclude(a=>a.Author)
                .ToList();

            //for (int i = 0; i < books.Count; i++)
            //{
            //    books[i].Category = _db.Categories.Find(books[i].Category_Id);
            //   // books[i].Publisher = _db.Publishers.Find(books[i].Publisher_Id);

            //}
            //foreach (var book in books)
            //{
            //    _db.Entry(book).Reference(u=>u.Publisher).Load();
            //}
            //foreach(var book in books)
            //{
            //    _db.Entry(book).Collection(b => b.BookAuthors).Load();
            //    foreach (var bookBookAuthor in book.BookAuthors)
            //    {
            //        _db.Entry(bookBookAuthor).Reference(a => a.Author).Load();
            //    }
            //}
            return View(books);
        }

        public IActionResult Upsert(int? id)
        {
            BookVM book=new BookVM();
            book.PublisherList = _db.Publishers.Select(p => new SelectListItem()
            {
                Text = p.Name,
                Value = p.Publisher_Id.ToString()
            });
            book.CategorieList = _db.Categories.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            if (id == null)
            {
                book.Book=new Book();
                return View(book);
            }

            book.Book = _db.Books.Find(id);
            if (book.Book == null)
                return NotFound();

            return View(book);
        }

        [HttpPost]
        public IActionResult Upsert(BookVM crBk)
        {
            if(ModelState.IsValid)
            {
                if (crBk.Book.Book_Id == 0)
                {
                    //crBk.Book.BookDetail_Id = 1;
                    _db.Add(crBk.Book);
                }
                else
                {
                    _db.Update(crBk.Book);
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(crBk);
        }

        public IActionResult Delete(int id)
        {
            var book = _db.Books.Find(id);
            _db.Books.Remove(book);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void PlayGround()
        {
            //var bookTemp = _db.Books.FirstOrDefault();
            //bookTemp.Price = 100;

            //var bookCollection = _db.Books;
            //double totalPrice = 0;
            //foreach(var book in bookCollection)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookList = _db.Books.ToList();
            //foreach(var book in bookList)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookCollection2 = _db.Books;
            //var bookCount1 = bookCollection2.Count();
            //var bookCount2 = _db.Books.Count();
            //int a = 0;
            //IEnumerable<Book> bookList = _db.Books;
            //var filter = bookList.Where(b => b.Price > 500).ToList();

            //IQueryable<Book> bookListQ = _db.Books;
            //var filterQ = bookListQ.Where(b => b.Price > 500).ToList();

            //var bookTemp1 = _db.Books.Include(b => b.BookDetail)
            //    .FirstOrDefault(b => b.Book_Id ==1002);
            //bookTemp1.BookDetail.NumberOfPages = 122;
            //_db.Books.Update(bookTemp1);
            //_db.SaveChanges();

            //var bookTemp2 = _db.Books.Include(b => b.BookDetail)
            //    .FirstOrDefault(b => b.Book_Id == 1002);
            //bookTemp2.BookDetail.Weight = 322;
            //_db.Books.Attach(bookTemp2);
            //_db.SaveChanges();

            var category = _db.Categories.FirstOrDefault();
            category.Name = "Test book";
            _db.Entry(category).State = EntityState.Modified;
            _db.SaveChanges();

            //var bookTemp2 = _db.Books.Include(b => b.BookDetail)
            //    .FirstOrDefault(b => b.Book_Id == 1002);
            //bookTemp2.BookDetail.Weight = 1322;
            //_db.Entry(bookTemp2.BookDetail).State = EntityState.Modified;
            //_db.SaveChanges();

            //var cat = _db.Categories.Find(10);
            //_db.Entry(cat).State = EntityState.Deleted;
            //_db.SaveChanges();

            //var cat = _db.Categories.Find(9);
            //var cat2 = _db.Categories.Where(c => c.Id == 9).AsNoTracking().First(); Not to track and not save  
            //cat.Name = "Change Tracker";
            //_db.SaveChanges();

            //var viewlist = _db.BookDetailsFormViews.ToList();
            //var viewlist2 = _db.BookDetailsFormViews.FirstOrDefault();
            //var viewList3 = _db.BookDetailsFormViews.Where(u => u.Price > 500);

            //var bookRaw = _db.Books.FromSqlRaw("Select * from Books").ToList();
            //int id = 1003;
            //var Raw1 = _db.Books.FromSqlInterpolated($"Select * From Books where Book_Id={id}").ToList();
            //var sp = _db.Books.FromSqlInterpolated($"EXEC dbo.getAllBookDetails {id}").ToList();

            var bookFilter = _db.Books.Include(b => b.BookAuthors.Where(a => a.Author_Id == 1)).ToList();
            var bookFilter2 = _db.Books.Include(b => b.BookAuthors.OrderByDescending(p => p.Author_Id == 1)).ToList().Take(5);

        }

        public IActionResult ManageAuthors(int id)
        {
            BookAuthorVM bookAuthor = new BookAuthorVM() 
            {
                BookAuthors = _db.BookAuthors.Include(u=>u.Author)
               .Include(u=>u.Book)
               .Where(b=>b.Book_Id==id).ToList(),

                BookAuthor= new BookAuthor()
                {
                    Book_Id=id
                },

                Book=_db.Books.Find(id)

            };
            List<int> listOfAuthors = bookAuthor.BookAuthors.Select(u => u.Author_Id).ToList();
            var tempList = _db.Authors.Where(a => !listOfAuthors.Contains(a.Author_Id)).ToList();
            bookAuthor.AuthorList = tempList.Select(i => new SelectListItem()
            {
                Value = i.Author_Id.ToString(),
                Text = i.FullName

            });

            return View(bookAuthor);
        }

        [HttpPost]
        public IActionResult ManageAuthors (BookAuthorVM bookAuthorVM)
        {
            if (bookAuthorVM.BookAuthor.Book_Id != 0 && bookAuthorVM.BookAuthor.Author_Id !=0) ;
            {
                _db.BookAuthors.Add(bookAuthorVM.BookAuthor);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(ManageAuthors) , new { @id = bookAuthorVM.BookAuthor.Book_Id });
        }

        public IActionResult RemoveAuthor (int authorId , BookAuthorVM bookAuthorVM)
        {
            int bookId = bookAuthorVM.Book.Book_Id;
            var ba = _db.BookAuthors.FirstOrDefault(b => b.Author_Id == authorId && b.Book_Id == bookId);
            _db.BookAuthors.Remove(ba);
            _db.SaveChanges();
            return RedirectToAction(nameof(ManageAuthors), new {@id =bookId });
        }

    }
}


