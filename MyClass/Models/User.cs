using System;
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        //------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Tên đầy đủ")]
        [Display(Name = "Tên đầy đủ")]
        public string Fullname { get; set; }
        //------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Tên tài khoản")]
        [Display(Name = "Tên tài khoản")]
        public string Username { get; set; }
        //------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        //------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        //------------------------------------//
        [Display(Name = "Giới tính")]
        public int Gender { get; set; }
        //------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Số điện thoại")]
        [Display(Name = "Số điện thoại")]
        public int Phone { get; set; }
        //------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [Display(Name = "Email")]
        public string Address { get; set; }
        //------------------------------------//
        [Display(Name = "Vai trò người dùng")]
        public string Roles { get; set; }
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
