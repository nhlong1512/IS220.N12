using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoriiCoffee.Models
{
    [Table("SanPhams")]
    public class SanPham
    {
        [Key]
        public int MaSP { get; set; }
        [Required]
        public string PhanLoai { get; set; }
    }
}