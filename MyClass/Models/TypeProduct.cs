using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("TypeProducts")]
   public class TypeProduct
    {
        public int Id { get; set; }
        //------------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập Tên Kiểu")]
        [Display(Name = "Tên kiểu")]
        public string Name { get; set; }
        //------------------------------------------//
        [Display(Name = "Tạo bởi")]
        public int? CreatedBy { get; set; }
        //---------------------------------------//
       
        [Display(Name = "Cập nhật bởi")]
        public int? UpdatedBy { get; set; }
        //---------------------------------------//
       
        [Display(Name = "Trạng thái")]
        public int Status { get; set; }
    }
}
