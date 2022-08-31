using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Links")]
    public class Link
    {
        [Key]
        public int Id { get; set; }
        //---------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Liên kết")]
        [Display(Name = "Liên kết")]
        public string Slug { get; set; }
        //---------------------------------------//
        [Display(Name = "Kiểu liên kết")]
        public String TypeLink { get; set; }
        //---------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Mã bảng")]
        [Display(Name = "Mã bảng")]
        public int TableId { get; set; }

    }
}
