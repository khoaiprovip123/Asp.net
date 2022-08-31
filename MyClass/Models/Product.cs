using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        //----------------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Tên sản phẩm")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        //----------------------------------------------//
        [Display(Name = "Liên kết")]
        public string Slug { get; set; }
        //----------------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Chi tiết")]
        [Display(Name = "Chi tiết")]
        public string Detail { get; set; }
        //----------------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Từ khóa tiêm kiếm")]
        [Display(Name = "Từ khóa tiêm kiếm")]
        public string Metakey { get; set; }
        //----------------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Từ khóa mô tả")]
        [Display(Name = "Từ khóa mô tả")]
        public string Metadesc { get; set; }
        //----------------------------------------------//
        [Display(Name = "Hình ảnh")]
        public string Img { get; set; }
        //----------------------------------------------//
        [Required(ErrorMessage = "Vui lòng chọn loại sản phẩm")]
        [Display(Name = "Loại sản phẩm")]
        public int CatId { get; set; }
        //----------------------------------------------//
        [Required(ErrorMessage = "Vui lòng chọn màu")]
        [Display(Name = "màu")]
        public string ColorId { get; set; }
        //----------------------------------------------//
        [Required(ErrorMessage = "Vui lòng chọn kiểu")]
        [Display(Name = "Kiểu")]
        public string TypeProductId { get; set; }
        //----------------------------------------------//
        [Display(Name = "Nhà cung cấp")]
        public string SupplierId { get; set; }
        //----------------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [Display(Name = "Số lượng")]
        public int Number { get; set; }
        //----------------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Giá")]
        [Display(Name = "Giá")]
        public decimal Price { get; set; }
        //----------------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Giá khuyến mãi")]
        [Display(Name = "Giá khuyến mãi")]
        public decimal Pricesale { get; set; }
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
        //public int? SuppliderId { get; set; }
    }
}
