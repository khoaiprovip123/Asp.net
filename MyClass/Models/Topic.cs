using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Topics")]
    public class Topic
    {
        [Key]
        public int Id { get; set; }
        //----------------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Tên chủ đề")]
        [Display(Name = "Tên chủ đề")]
        public String Name { get; set; }
        //----------------------------------------------//
        [Display(Name = "Liên kết")]
        public String Slug { get; set; }
        //----------------------------------------------//
        [Display(Name = "Mã cấp cha")]
        public int?  ParentId { get; set; }
        //----------------------------------------------//
        [Display(Name = "Sắp xếp")]
        public int? Order { get; set; }
        //---------------------------------------//
        //[Required(ErrorMessage = "Vui lòng nhập kiểu")]
        [Display(Name = "Kiểu")]
        public string Type { get; set; }
        //---------------------------------------//
        [Display(Name = "Bảng")]
        public int? Table { get; set; }
        //---------------------------------------//
        [Display(Name = "Chức Vụ")]
        public string Position { get; set; }
        //---------------------------------------//
     
        [Display(Name = "Từ khóa tiêm kiếm")]
        public string Metakey { get; set; }
        //----------------------------------------------//
       
        [Display(Name = "Từ khóa mô tả")]
        public string Metadesc { get; set; }
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
