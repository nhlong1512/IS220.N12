using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoriiCoffee.Models
{
    [Table("KhuyenMais")]
    public class KhuyenMai
    {
        [Key]
        public int MaKM { get; set; }
        public int PhanTramKM { get; set; }
    }
}