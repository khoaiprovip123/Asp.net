using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        //---------------------------------------//
       
        [Display(Name = "Mã người dùng")]
        public int UserId { get; set; }
        //---------------------------------------//
       
        [Display(Name = "Tên đơn hàng")]
        public string ReceiverName { get; set; }
        //---------------------------------------//
        [Display(Name = "Số điện thoại")]
        public string ReceiverPhone { get; set; }
        //---------------------------------------//
        [Display(Name = "Email")]
        public string ReceiverEmail { get; set; }
        //---------------------------------------//
        [Display(Name = "Địa chỉ")]
        public string ReceiverAdress { get; set; }
        //---------------------------------------//
        [Display(Name = "Ghi chú")]
        public string Note { get; set; }

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
