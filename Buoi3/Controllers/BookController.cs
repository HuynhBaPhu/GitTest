using Buoi3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buoi3.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult ListBook()
        {
            BookManagerContext context = new BookManagerContext();
            var listBook = context.Books.ToList();
            return View(listBook);
        }
        [Authorize]
        public ActionResult Buy(int id)
        {
            BookManagerContext context = new BookManagerContext();
            Book book = context.Books.SingleOrDefault(p => p.ID == id);
            if(book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            BookManagerContext context = new BookManagerContext();
            Book db = context.Books.FirstOrDefault(p => p.ID == id);
            return View(db);
        }

        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public ActionResult Create(Book b)
        {
            BookManagerContext context = new BookManagerContext();
            context.Books.AddOrUpdate(b);
            context.SaveChanges();
            return RedirectToActionPermanent("ListBook");
        }
        public ActionResult Edit(int id)
        {
            BookManagerContext context = new BookManagerContext();
            Book book = context.Books.FirstOrDefault(p => p.ID == id);
            if(book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book b)
        {
            BookManagerContext context = new BookManagerContext();
            Book book = context.Books.FirstOrDefault(p => p.ID == b.ID);
            book.Title = b.Title;
            book.Description = b.Description;
            book.Author = b.Author;
            book.Images = b.Images;
            book.Price = b.Price;
            context.Books.AddOrUpdate(book);
            context.SaveChanges();
            return RedirectToActionPermanent("ListBook");
        }
        public ActionResult Delete(int id)
        {
            BookManagerContext context = new BookManagerContext();
            Book b = context.Books.FirstOrDefault(p => p.ID == id);
            if(b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Book b)
        {
            BookManagerContext context = new BookManagerContext();
            Book book = context.Books.FirstOrDefault(p => p.ID == b.ID);
            context.Books.Remove(book);
            context.SaveChanges();
            return RedirectToActionPermanent("ListBook");
        }
    }
}