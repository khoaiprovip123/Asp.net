using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        //---------------------------------------//
        [Display(Name = "Mã đơn hàng")]
        public int Orderid { get; set; }
        //---------------------------------------//
        [Display(Name = "Mã sản phẩm")]
        public int Productid { get; set; }
        //---------------------------------------//
        [Display(Name = "Giá")]
        public decimal Price { get; set; }
        //---------------------------------------//
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }
        //---------------------------------------//
        [Display(Name = "Tổng tiền")]
        public decimal Amount { get; set; }
        //---------------------------------------//
        [Display(Name = "Giảm giá")]
        public decimal Sale { get; set; }
    }
}
