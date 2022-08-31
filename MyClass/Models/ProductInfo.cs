using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    public class ProductInfo
    {
        public int Id { get; set; }
        //----------------------------------------------//
        public string Name { get; set; }
        //----------------------------------------------// [Display(Name = "Liên kết")]
        public string CatName { get; set; }
        //----------------------------------------------//
        public string Slug { get; set; }
        //----------------------------------------------//

        public string Detail { get; set; }
        //----------------------------------------------// [Display(Name = "Từ khóa tiêm kiếm")]
        public string Metakey { get; set; }
        //----------------------------------------------//  [Display(Name = "Từ khóa mô tả")]
        public string Metadesc { get; set; }
        //----------------------------------------------//[Display(Name = "Hình ảnh")]
        public string Img { get; set; }
        //----------------------------------------------// [Display(Name = "Loại sản phẩm")]
        public int CatId { get; set; }
        //----------------------------------------------//[Display(Name = "màu")]
        public string ColorId { get; set; }
        //----------------------------------------------//[Display(Name = "Kiểu")]
        public string TypeProductId { get; set; }
        //----------------------------------------------//[Display(Name = "Nhà cung cấp")]
        public string SupplierId { get; set; }
        //----------------------------------------------//[Display(Name = "Số lượng")]
        public int Number { get; set; }
        //----------------------------------------------//[Display(Name = "Giá")]
        public decimal Price { get; set; }
        //----------------------------------------------// [Display(Name = "Giá khuyến mãi")]
        public decimal Pricesale { get; set; }
        //------------------------------------------// [Display(Name = "Tạo bởi")]
        public int? CreatedBy { get; set; }
        //---------------------------------------//[Display(Name = "Tạo vào lúc")]
        public DateTime? CreatedAt { get; set; }
        //----------------------------------------//  [Display(Name = "Cập nhật bởi")]
        public int? UpdatedBy { get; set; }
        //---------------------------------------// [Display(Name = "Cập nhật vào lúc")]
        public DateTime? UpdatedAt { get; set; }
        //----------------------------------------// [Display(Name = "Trạng thái")]
        public int Status { get; set; }

    }
}
