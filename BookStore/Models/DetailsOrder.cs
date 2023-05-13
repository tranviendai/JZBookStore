using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class DetailsOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        
        public int index { get; set; }
        public int billID { get; set; }
        [ForeignKey("billID")]
        public Bill Bill { get; set; }

        public int bookID { get; set; }
        [ForeignKey("bookID")]
        public Book Book { get; set; }

        [Display(Name ="Số lượng")]
        public int quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        [Display(Name ="Đơn giá")]
        [DataType(DataType.Currency)]
        public decimal price { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        [Display(Name = "Thành Tiền")]
        [DataType(DataType.Currency)]        
        public decimal totalPrice { get; set; }


    }
}