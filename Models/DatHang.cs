using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoriiCoffee.Models
{
    [Table("DatHangs")]
    public class DatHang
    {
        [Key]
        public int MaDH { get; set; }
        public int MaHD { get; set; }
        [Required]
        public string TrangThai { get; set; }
        [Required]
        public string DiaChiNH { get; set; }//Địa chỉ nhận hàng
    }
}