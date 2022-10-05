using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoriiCoffee.Models
{
 
    [Table("ChiTietSPs")]
    public class ChiTietSP
    {
        [Key]
        public int MaSP { get; set; }
        [Required]
        public string TenSP { get; set; }
        public int Gia { get; set; }
        [Required]
        public string Size { get; set; }
        public int MaKM { get; set; }
    }
}