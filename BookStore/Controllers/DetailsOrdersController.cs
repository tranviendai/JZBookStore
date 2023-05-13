using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using PagedList;

namespace BookStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DetailsOrdersController : Controller
    {
        private KhachHang db = new KhachHang();

        // GET: DetailsOrders
        public ActionResult Index(int? page, string nameCustomer, string currentFilter, string searchString, string title, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameCustomerSortParm = String.IsNullOrEmpty(sortOrder) ? "nameCustomer_desc" : "";
            ViewBag.TitleSortParm = sortOrder == "title" ? "title_desc" : "title";
            ViewBag.QuantitySortParm = sortOrder == "quantity" ? "quantity_desc" : "quantity";
            ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.TotalPriceSortParm = sortOrder == "totalPrice" ? "totalPrice_desc" : "totalPrice";
            var dtorders = db.DetailsOrders.Include(a => a.Bill).Include(b => b.Book);

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
                case "nameCustomer_desc":
                    dtorders = dtorders.OrderByDescending(c => c.Bill.nameCustomer);
                    break;
                case "title":
                    dtorders = dtorders.OrderBy(c => c.Book.title);
                    break;
                case "title_desc":
                    dtorders = dtorders.OrderByDescending(c => c.Book.title);
                    break;
                case "quantity":
                    dtorders = dtorders.OrderBy(c => c.quantity);
                    break;
                case "quantity_desc":
                    dtorders = dtorders.OrderByDescending(c => c.quantity);
                    break;
                case "price":
                    dtorders = dtorders.OrderBy(c => c.price);
                    break;
                case "price_desc":
                    dtorders = dtorders.OrderByDescending(c => c.price);
                    break;
                case "totalPrice":
                    dtorders = dtorders.OrderBy(c => c.totalPrice);
                    break;
                case "totalPrice_desc":
                    dtorders = dtorders.OrderByDescending(c => c.totalPrice);
                    break;
            }
            if (!string.IsNullOrEmpty(nameCustomer))
            {
                dtorders = dtorders.Where(c => c.Bill.nameCustomer.Contains(nameCustomer));
            }

            if (!string.IsNullOrEmpty(title))
            {
                dtorders = dtorders.Where(c => c.Book.title.Contains(title));
            }
            dtorders = dtorders.OrderBy(c => c.Bill.nameCustomer);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dtorders.ToPagedList(pageNumber, pageSize));
        }

        // GET: DetailsOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsOrder detailsOrder = db.DetailsOrders.Find(id);
            if (detailsOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.billID = new SelectList(db.Bills, "billID", "nameCustomer", detailsOrder.billID);
            ViewBag.bookID = new SelectList(db.Books, "bookID", "title", detailsOrder.bookID);
            return View(detailsOrder);
        }

        // GET: DetailsOrders/Create
        public ActionResult Create()
        {
            ViewBag.billID = new SelectList(db.Bills, "billID", "nameCustomer");
            ViewBag.bookID = new SelectList(db.Books, "bookID", "title");
            return View();
        }

        // POST: DetailsOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "index,billID,bookID,quantity,price,totalPrice")] DetailsOrder detailsOrder)
        {
            if (ModelState.IsValid)
            {
                db.DetailsOrders.Add(detailsOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.billID = new SelectList(db.Bills, "billID", "nameCustomer", detailsOrder.billID);
            ViewBag.bookID = new SelectList(db.Books, "bookID", "title", detailsOrder.bookID);
            return View(detailsOrder);
        }

        // GET: DetailsOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsOrder detailsOrder = db.DetailsOrders.Find(id);
            if (detailsOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.billID = new SelectList(db.Bills, "billID", "nameCustomer", detailsOrder.billID);
            ViewBag.bookID = new SelectList(db.Books, "bookID", "title", detailsOrder.bookID);
            return View(detailsOrder);
        }

        // POST: DetailsOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "index,billID,bookID,quantity,price,totalPrice")] DetailsOrder detailsOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detailsOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.billID = new SelectList(db.Bills, "billID", "nameCustomer", detailsOrder.billID);
            ViewBag.bookID = new SelectList(db.Books, "bookID", "title", detailsOrder.bookID);
            return View(detailsOrder);
        }

        // GET: DetailsOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsOrder detailsOrder = db.DetailsOrders.Find(id);
            if (detailsOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.billID = new SelectList(db.Bills, "billID", "nameCustomer", detailsOrder.billID);
            ViewBag.bookID = new SelectList(db.Books, "bookID", "title", detailsOrder.bookID);
            return View(detailsOrder);
        }

        // POST: DetailsOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetailsOrder detailsOrder = db.DetailsOrders.Find(id);
            db.DetailsOrders.Remove(detailsOrder);
            db.SaveChanges();
            ViewBag.billID = new SelectList(db.Bills, "billID", "nameCustomer", detailsOrder.billID);
            ViewBag.bookID = new SelectList(db.Books, "bookID", "title", detailsOrder.bookID);
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
