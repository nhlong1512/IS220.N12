using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoriiCoffee.Models
{
    [Table("CuaHangs")]
    public class CuaHang
    {
        [Key]
        public int MaCH { get; set; }
        [Required]
        public string TenCH { get; set; }
        [Required]
        public string SDT { get; set; }
        public int MaQuanLy { get; set; }
        [Required]
        public string DiaChi { get; set; }
    }
}