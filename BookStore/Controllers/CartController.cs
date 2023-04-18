using BookStore.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BookStore.Controllers
{
    [Authorize(Roles = "KhachHang")]
    public class CartController : Controller
    {
        KhachHang db = new KhachHang();
        public Cart getCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        //Trang thêm vào item vào để mua nhiều vé
        public ActionResult AddCart(int id)
        {
            var pro = db.Books.SingleOrDefault(s => s.bookID == id);
            if (pro != null)
            {
                getCart().Add(pro);
            }
            return RedirectToAction("ShowCart", "Cart");
        }
        //Trang Mua vé
        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("ShowCart", "Cart");
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult UpdateCart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id = int.Parse(form["bookID"]);
            int quantity = int.Parse(form["quantity"]);
            cart.Update(id, quantity);
            if (quantity < 1)
            {
                cart.ClearCart();
            }
            if (quantity > 10)
            {
                cart.ClearCart();
            }
            return RedirectToAction("ShowCart", "Cart");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove(id);
            return RedirectToAction("ShowCart", "Cart");
        }
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                Bill bill = new Bill();
                bill.date = DateTime.Now;
                bill.deliveryDate = DateTime.Now;
                bill.Id = User.Identity.GetUserId();
                bill.delivered = false;
                bill.payment = Convert.ToBoolean(form["payment"]);
                bill.deliveryAddress = form["address"];
                bill.nameCustomer = form["shipName"];
                bill.shipPhone = form["shipPhone"];
                bill.deliveryForm = Convert.ToBoolean(form["deliveryForm"]);
                bill.VoucherID = Convert.ToInt32(form["voucher"]);
                bill.price = cart.Total();
                db.Bills.Add(bill);
                foreach (var item in cart.Items)
                {
                    DetailsOrder detailOrder = new DetailsOrder();
                    detailOrder.billID = bill.billID;
                    detailOrder.bookID = item.book.bookID;
                    detailOrder.quantity = item.quantity;
                    detailOrder.totalPrice = (item.book.price) * detailOrder.quantity;
                    db.DetailsOrders.Add(detailOrder);
                }
                db.SaveChanges();
                cart.ClearCart();
            }
            catch
            {
                return Content("Không Thành Công, Mời Bạn Check Lại Thông Tin");
            }
            return RedirectToAction("Index", "Bills");
        }
    }
}