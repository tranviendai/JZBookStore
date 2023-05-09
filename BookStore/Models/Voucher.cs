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
        [DisplayFormat(DataFormatString = "{0:N0}")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Vui lòng nhập giá tiền (Voucher)")]
        [Display(Name = "Giá Voucher")]
        public decimal Price { get; set; }
        public ICollection<Bill> bills { get; set; }
    }
}