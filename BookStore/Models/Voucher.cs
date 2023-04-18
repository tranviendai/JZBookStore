using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Voucher
    {
        [Key]
        public int VoucherID { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<Bill> bills { get; set; }
    }
}