using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Categorys")]
    public class Category
    {
        [Key]
        public int Id {get;set;}
        //-------------------------------------//
        [Required(ErrorMessage ="Vui lòng nhập tên loại")]
        [Display(Name ="Tên loại sản phẩm")]
        public string Name { get; set; }
        //---------------------------------------//
        [Display(Name = "Liên Kết")]
        public string Slug { get; set; }
        //------------------------------------------//
        [Display(Name = "Mã cấp cha")]
        public int? ParentId { get; set; }
        //------------------------------------------//
        [Display(Name = "Sắp xếp")]
        public int? Orders { get; set; }
        //----------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập khóa mô tả")]
        [Display(Name = "Từ khóa mô tả")]
        public string MetaDesc { get; set; }
        //----------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập từ khóa tìm kiếm")]
        [Display(Name = "Từ khóa tìm kiếm")]
        public string MetaKey { get; set; }
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
