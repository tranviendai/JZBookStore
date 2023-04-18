using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using Microsoft.AspNet.Identity;
using PagedList;

namespace BookStore.Controllers
{
    [Authorize(Roles = "Admin,KhachHang")]
    public class BillsController : Controller
    {
        private KhachHang db = new KhachHang();

        // GET: Bills
        public ActionResult Index(int? page, string fullname, string currentFilter, string searchString, string namecustomer, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FullnameSortParm = String.IsNullOrEmpty(sortOrder) ? "fullname_desc" : "";
            ViewBag.DateSortParm = sortOrder == "date" ? "date_desc" : "date";
            ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.DeliveredSortParm = sortOrder == "delivered" ? "delivered_desc" : "delivered";
            ViewBag.DeliveryDateSortParm = sortOrder == "deliveryDate" ? "deliveryDate_desc" : "deliveryDate";
            ViewBag.NameCustomerSortParm = sortOrder == "nameCustomer" ? "nameCustomer_desc" : "nameCustomer";
            ViewBag.DeliveryAddressSortParm = sortOrder == "deliveryAddress" ? "deliveryAddress_desc" : "deliveryAddress";
            ViewBag.ShipPhoneDateSortParm = sortOrder == "shipPhone" ? "shipPhone_desc" : "shipPhone";
            ViewBag.PaymentSortParm = sortOrder == "payment" ? "payment_desc" : "payment";
            ViewBag.DeliveryFormSortParm = sortOrder == "deliveryForm" ? "deliveryForm_desc" : "deliveryForm";
            var bills = db.Bills.Include(u => u.KhachHang).Include(v=>v.Voucher);
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
                case "fullname_desc":
                    bills = bills.OrderByDescending(c => c.KhachHang.FullName);
                    break;
                case "date":
                    bills = bills.OrderBy(c => c.date);
                    break;
                case "date_desc":
                    bills = bills.OrderByDescending(c => c.date);
                    break;
                case "price":
                    bills = bills.OrderBy(c => c.price);
                    break;
                case "price_desc":
                    bills = bills.OrderByDescending(c => c.price);
                    break;
                case "delivered":
                    bills = bills.OrderBy(c => c.delivered);
                    break;
                case "delivered_desc":
                    bills = bills.OrderByDescending(c => c.delivered);
                    break;
                case "deliveryDate":
                    bills = bills.OrderBy(c => c.deliveryDate);
                    break;
                case "deliveryDate_desc":
                    bills = bills.OrderByDescending(c => c.deliveryDate);
                    break;
                case "nameCustomer":
                    bills = bills.OrderBy(c => c.nameCustomer);
                    break;
                case "nameCustomer_desc":
                    bills = bills.OrderByDescending(c => c.nameCustomer);
                    break;
                case "deliveryAddress":
                    bills = bills.OrderBy(c => c.deliveryAddress);
                    break;
                case "deliveryAddress_desc":
                    bills = bills.OrderByDescending(c => c.deliveryAddress);
                    break;
                case "shipPhone":
                    bills = bills.OrderBy(c => c.shipPhone);
                    break;
                case "shipPhone_desc":
                    bills = bills.OrderByDescending(c => c.shipPhone);
                    break;
                case "payment":
                    bills = bills.OrderBy(c => c.payment);
                    break;
                case "payment_desc":
                    bills = bills.OrderByDescending(c => c.payment);
                    break;
                case "deliveryForm":
                    bills = bills.OrderBy(c => c.deliveryForm);
                    break;
                case "deliveryForm_desc":
                    bills = bills.OrderByDescending(c => c.deliveryForm);
                    break;
            }
            if (!string.IsNullOrEmpty(fullname))
            {
                bills = bills.Where(c => c.KhachHang.FullName.Contains(fullname));
            }

            bills = bills.OrderBy(c => c.KhachHang.FullName);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(bills.ToPagedList(pageNumber, pageSize));
        }

        // GET: Bills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // GET: Bills/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "name");
            ViewBag.Id = new SelectList(db.Bills, "Id", "FullName");
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "billID,date,price,delivered,deliveryDate,nameCustomer,deliveryAddress,shipPhone,payment,deliveryForm,Id,VoucherID")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                bill.delivered = false;
                bill.price = 9999999; //code công thức sau nha
                bill.date = DateTime.Now;
                bill.Id = User.Identity.GetUserId();
                db.Bills.Add(bill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VoucherID = new SelectList(db.Subjects, "VoucherID", "name");
            ViewBag.Id = new SelectList(db.Bills, "Id", "FullName", bill.Id);
            return View(bill);
        }

        // GET: Bills/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Bills, "Id", "FullName", bill.Id);
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "billID,date,price,delivered,deliveryDate,nameCustomer,deliveryAddress,shipPhone,payment,deliveryForm,Id")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Bills, "Id", "FullName", bill.Id);
            return View(bill);
        }

        // GET: Bills/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bill bill = db.Bills.Find(id);
            db.Bills.Remove(bill);
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
