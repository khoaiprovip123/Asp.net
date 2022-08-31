using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Contacts")]

    public class Contact
    {
        [Key]
        public int Id { get; set; }
        //----------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [Display(Name = "Tên đầy đủ")]
        public string Fullname { get; set; }
        //----------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        //---------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Số điện thoại")]
        [Display(Name = "Số điện thoại")]
        public int Phone { get; set; }
        //---------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        //---------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        [Display(Name = "Nội dung")]
        public string Content { get; set; }
        //------------------------------------------//
        [Display(Name = "Tạo bởi")]
        public int? CreatedBy { get; set; }
        //---------------------------------------//
        [Display(Name = "Tạo vào lúc")]
        public DateTime? CreatedAt { get; set; }
        //----------------------------------------//
        [Display(Name = "Cập nhật bởi")]
        public int? UpdatedBy { get; set; }
        //---------------------------------------//
        [Display(Name = "Cập nhật vào lúc")]
        public DateTime? UpdatedAt { get; set; }
        //----------------------------------------//
        [Display(Name = "Trạng thái")]
        public int Status { get; set; }
    }
}
