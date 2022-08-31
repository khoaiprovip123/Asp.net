using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Configs")]
    public class Config
    {
        [Key]
        public int Id { get; set; }
        //------------------------------//
        [Required(ErrorMessage ="Vui lòng nhập tên")]
        [Display(Name="Tên")]
        public String Name { get; set; }
        //------------------------------//
        [Required(ErrorMessage = "Vui lòng nhập giá trị")]
        [Display(Name = "Tên")]
        public String Value { get; set; }
    }
}
