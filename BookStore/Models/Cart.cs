using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class CartItem
    {
        public Book book { get; set; }
        [Required]
        public int quantity { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Add(Book _book, int quantity = 1)
        {
            var item = items.FirstOrDefault(s => s.book.bookID == _book.bookID);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    book = _book,
                    quantity = quantity
                });
            }
            else
            {
                item.quantity += quantity;
            }
        }

        public void Update(int id, int quantity)
        {
            var item = items.Find(s => s.book.bookID == id);
            if (item != null)
            {
                item.quantity = quantity;
            }
        }
        public void Remove(int id)
        {
            items.RemoveAll(s => s.book.bookID == id);
        }
        public decimal Total()
        {
            var total = items.Sum(s => s.book.price * s.quantity);
            return total;
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
}