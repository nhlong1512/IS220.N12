using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoriiCoffee.Models
{
    [Table("NhanViens")]
    public class NhanVien
    {
        [Key]
        public int ID { get; set; }
        public int Luong { get; set; }
    }
}