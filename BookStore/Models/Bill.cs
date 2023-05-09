using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int billID { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name ="Ngày lập hóa đơn")]
        [DisplayFormat(DataFormatString = "{0:dd/M/yyyy HH:mm}")]
        public DateTime date { get; set; }

        [Display(Name ="Tổng hóa đơn")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        [DataType(DataType.Currency)]
        public decimal price { get; set; }

        [Display(Name ="Đã giao")]
        public bool delivered { get; set; }

        [Display(Name ="Ngày giao")]
        [DataType(DataType.Date)]
        public DateTime deliveryDate { get; set; }

        [StringLength(50)]
        [Display(Name ="Tên Người Nhận")]
        public string nameCustomer { get; set; }

        [StringLength(50)]
        [Display(Name ="Địa chỉ nhận")]
        public string deliveryAddress { get; set; }
        [StringLength(15)]
        [Display(Name ="Điện thoại người nhận")]
        public string shipPhone { get; set; }
        [Display(Name ="Hình thức thanh toán")]
        public bool payment { get; set; }
        [Display(Name = "Hình thức giao hàng")]
        public bool deliveryForm { get; set; }

        public int VoucherID { get; set; }
        [ForeignKey("VoucherID")]
        public Voucher Voucher { get; set; }

        [Display(Name ="Mã khách hàng")]
        public string Id { get; set; }
        [ForeignKey("Id")]
        public ApplicationUser KhachHang { get; set; }

        public ICollection<DetailsOrder> detailsOrders { get; set; }
    }
}