using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Sliders")]
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        //----------------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [Display(Name = "Tên")]
        public string Name { get; set; }
        //----------------------------------------------//
        [Display(Name = "Liên kết")]
        public string Link { get; set; }
        //----------------------------------------------//
        [Display(Name = "Hình ảnh")]
        public string Img { get; set; }
        //----------------------------------------------//
        [Display(Name = "Chức vụ")]
        public string Position { get; set; }
        //----------------------------------------------//
        [Display(Name = "Sắp xếp")]
        public int Orders { get; set; }
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
