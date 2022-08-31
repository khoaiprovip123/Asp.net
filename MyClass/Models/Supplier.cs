using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Suppliers")]
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        //----------------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Tên nhà cung cấp")]
        [Display(Name = "Nhà cung cấp")]
        public string Name { get; set; }
        //----------------------------------------------//
        [Display(Name = "Liên kết")]
        public string Link { get; set; }
        //----------------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [Display(Name = "Emil")]
        public string Email { get; set; }
        //----------------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Sô điện thoại")]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }
        //----------------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Địa chỉ")]
        [Display(Name = "Địa chỉ")]
        public string Adress { get; set; }
        //----------------------------------------------//
        [Display(Name = "Liên kết nhà cung cấp")]
        public string UrlSite { get; set; }
        //----------------------------------------------//
        [Display(Name = "Hình ảnh")]
        public string Img { get; set; }
        //----------------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Từ khóa tìm kiếm")]
        [Display(Name = "Từ khóa tiêm kiếm")]
        public string Metakey { get; set; }
        //----------------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Từ khóa mô tả")]
        [Display(Name = "Từ khóa mô tả")]
        public string Metadesc { get; set; }
        //------------------------------------------//
        [Display(Name = "Sắp xếp")]
        public int? Orders { get; set; }
        //---------------------------------------//
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
