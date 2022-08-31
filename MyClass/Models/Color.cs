using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Colors")]
    public class Color
    {
        [Key]

        public int Id { get; set; }
        //--------------------------------------//
        [Required(ErrorMessage ="Vui lòng nhập tên màu")]
        [Display(Name ="Tên màu")]
        public string Name { get; set; }
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
