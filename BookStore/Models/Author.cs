using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int authorID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="Vui lòng nhập họ tên")]
        [Display(Name ="Họ Tên")]
        public string name { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập địa chỉ")]
        [StringLength(100)]
        [Display(Name = "Địa Chỉ")]
        public string address { get; set; }

        [StringLength(15)]
        [Display(Name ="Số Điện Thoại")]
        [DataType(DataType.PhoneNumber)]
        public string phone { get; set; }

        public ICollection<Book> Book { get; set; }

    }
}