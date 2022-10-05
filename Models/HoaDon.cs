using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoriiCoffee.Models
{
    [Table("HoaDons")]
    public class HoaDon
    {
        [Key]
        public int MaHD { get; set; }
        public int MaNV { get; set; }
        public int MaKH { get; set; }
        public int MaCH { get; set; }
        public DateTime NgayHD { get; set; }
        public int TongTien { get; set; }
    }
}