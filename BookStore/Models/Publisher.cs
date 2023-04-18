using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Publisher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int publisherID { get; set; }

        [StringLength(50)]
        [Display(Name ="Họ Tên")]
        [Required(ErrorMessage ="Vui lòng nhập họ tên")]
        public string name { get; set; }

        [StringLength(150)]
        [Display(Name ="Địa chỉ")]
        [Required(ErrorMessage ="Vui lòng nhập địa chỉ")]
        public string address { get; set; }

        [StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Số điện thoại")]
        [Required(ErrorMessage ="Vui lòng nhập số điện thoại")]
        public string phone { get; set; }
        public ICollection<Book> books { get; set; }

    }
}