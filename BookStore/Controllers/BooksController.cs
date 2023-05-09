using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using BookStore.Models;
using Microsoft.Owin.BuilderProperties;
using PagedList;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private KhachHang db = new KhachHang();

        // GET: Books
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? page, string publishername, string currentFilter, string searchString, string subject, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.PublishernameSortParm = String.IsNullOrEmpty(sortOrder) ? "publishername_desc" : "";
            ViewBag.SubjectSortParm = sortOrder == "subject" ? "subject_desc" : "subject";
            ViewBag.TitleSortParm = sortOrder == "title" ? "title_desc" : "title";
            ViewBag.UnitSortParm = sortOrder == "unit" ? "unit_desc" : "unit";
            ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.DescriptionSortParm = sortOrder == "description" ? "description_desc" : "description";
            ViewBag.ImageSortParm = sortOrder == "image" ? "image_desc" : "image";
            ViewBag.UpdateDateSortParm = sortOrder == "updateDate" ? "updateDate_desc" : "updateDate";
            ViewBag.SellNumberSortParm = sortOrder == "sellNumber" ? "sellNumber_desc" : "sellNumber";
            var books = db.Books.Include(p=>p.publisher).Include(s=>s.subject);

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            switch (sortOrder)
            {
                case "publishername_desc":
                    books = books.OrderByDescending(c => c.publisher.name);
                    break;
                case "subject":
                    books = books.OrderBy(c => c.subject.name);
                    break;
                case "subject_desc":
                    books = books.OrderByDescending(c => c.subject.name);
                    break;
                case "title":
                    books = books.OrderBy(c => c.title);
                    break;
                case "title_desc":
                    books = books.OrderByDescending(c => c.title);
                    break;
                case "unit":
                    books = books.OrderBy(c => c.unit);
                    break;
                case "unit_desc":
                    books = books.OrderByDescending(c => c.unit);
                    break;
                case "price":
                    books = books.OrderBy(c => c.price);
                    break;
                case "price_desc":
                    books = books.OrderByDescending(c => c.price);
                    break;
                case "description":
                    books = books.OrderBy(c => c.description);
                    break;
                case "description_desc":
                    books = books.OrderByDescending(c => c.description);
                    break;
                case "image":
                    books = books.OrderBy(c => c.image);
                    break;
                case "image_desc":
                    books = books.OrderByDescending(c => c.image);
                    break;
                case "updateDate":
                    books = books.OrderBy(c => c.updateDate);
                    break;
                case "updateDate_desc":
                    books = books.OrderByDescending(c => c.updateDate);
                    break;
                case "sellNumber":
                    books = books.OrderBy(c => c.sellNumber);
                    break;
                case "sellNumber_desc":
                    books = books.OrderByDescending(c => c.sellNumber);
                    break;
            }
            if (!string.IsNullOrEmpty(publishername))
            {
                books = books.Where(c => c.publisher.name.Contains(publishername));
            }

            if (!string.IsNullOrEmpty(subject))
            {
                books = books.Where(c => c.subject.name.Contains(subject));
            }
            books = books.OrderBy(c => c.publisher.name);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(books.ToPagedList(pageNumber, pageSize));

        }

        // GET: Books/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = await db.Books.Include(p => p.publisher).Include(p => p.subject)
                        .FirstOrDefaultAsync(m => m.bookID == id); if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin")]

        public ActionResult Create()
        {
            ViewBag.publisherID = new SelectList(db.Publishers, "publisherID", "name");
            ViewBag.subjectID = new SelectList(db.Subjects, "subjectID", "name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public ActionResult Create([Bind(Include = "bookID,title,unit,price,description,image,updateDate,sellNumber,subjectID,publisherID")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.updateDate = DateTime.Now;
               
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.publisherID = new SelectList(db.Publishers, "publisherID", "name", book.publisherID);
            ViewBag.subjectID = new SelectList(db.Subjects, "subjectID", "name", book.subjectID);
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.publisherID = new SelectList(db.Publishers, "publisherID", "name", book.publisherID);
            ViewBag.subjectID = new SelectList(db.Subjects, "subjectID", "name", book.subjectID);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "bookID,title,unit,price,description,image,updateDate,sellNumber,subjectID,publisherID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.publisherID = new SelectList(db.Publishers, "publisherID", "name", book.publisherID);
            ViewBag.subjectID = new SelectList(db.Subjects, "subjectID", "name", book.subjectID);
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
