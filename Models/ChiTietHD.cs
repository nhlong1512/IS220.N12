using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoriiCoffee.Models
{
    [Table("ChiTietHDs")]
    public class ChiTietHD
    {
        [Key]
        public int MaHD { get; set; }
        public int MaSP { get; set; }
        public int SoLuong { get; set; }
        public int ThanhTien { get; set; }
    }
}