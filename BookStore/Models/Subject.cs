using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int subjectID { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập tên chủ đề")]
        [StringLength(50)]
        [Display(Name ="Tên Chủ Đề")]
        public string name { get; set; }

        public ICollection<Book> books { get; set; }
    }
}