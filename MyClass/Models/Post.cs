using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int Id { get; set; }

        //-------------------------------------------//
        [Display(Name = "Mã bài viết")]
        public int Topid { get; set; }
        //-------------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        //-------------------------------------------//
        [Display(Name = "Liên kết")]
        public string Slug { get; set; }

        //-------------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Chi tiết")]
        [Display(Name = "Chi tiết")]
        public string Detail { get; set; }
        //-------------------------------------------//
        [Display(Name = "Hình ảnh")]
        public string Img { get; set; }
        //-------------------------------------------//
        [Display(Name = "Kiểu bài viết")]
        public string PostType { get; set; }
        //-------------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập từ khóa tiềm kiếm")]
        [Display(Name = "Từ khóa tiềm kiếm")]
        public string Metakey { get; set; }
        //-------------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập từ khóa Mô tả")]
        [Display(Name = "Từ khóa mô tả")]
        public string MetaDesc { get; set; }

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
