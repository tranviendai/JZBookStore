using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using BookStore.Models;
using Microsoft.Owin.BuilderProperties;
using PagedList;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        KhachHang db = new KhachHang();
        public ActionResult Index()
        {
            var detailorderList = GetDetailOrderList();
            return View(detailorderList);
        }
        public List<DetailsOrder> GetDetailOrderList()
        {
            var detailorders = new List<DetailsOrder>() {
            new DetailsOrder(){ billID=1, bookID=1, quantity=55, price=55000, totalPrice=3025000 },
            new DetailsOrder(){ billID=2, bookID=2, quantity=25, price=120000, totalPrice=3000000 },
            new DetailsOrder(){ billID=3, bookID=3, quantity=78, price=34000, totalPrice=2652000 },
            new DetailsOrder(){ billID=4, bookID=4, quantity=43, price=25000, totalPrice=1075000 },
            new DetailsOrder(){ billID=5, bookID=5, quantity=92, price=30000, totalPrice=2760000 }};
            return detailorders;
        }
        public ActionResult Home(int? page, string searchBy, string titleBook, string publisherBook, string searchNKH)
        {
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var books = db.Books.Include(p => p.publisher).Include(s => s.subject);

            IPagedList<Book> lb = null;
            lb = books.ToList().OrderBy(t => t.updateDate).ToPagedList(pageIndex, pageSize);
            if (searchBy == "Tìm Chuyến Bay")
            {
                return View(books.Where(s => s.title.StartsWith(titleBook)).ToList().ToPagedList(pageIndex, pageSize).
                    Where(s => s.publisher.name.StartsWith(publisherBook)).ToList().ToPagedList(pageIndex, pageSize));
            }
            return View(lb);
        }

    }
}