using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MoriiCoffee.Models
{
    [Table("NguoiDungs")]
    public class NguoiDung
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string HoTen { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]

        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]

        public string Password { get; set; }
        [NotMapped]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }


        public string SDT { get; set; }
        public string Role { get; set; }
        public DateTime? NgDK { get; set; }
        public DateTime? NgSinh { get; set; }
        public string GioiTinh { get; set; }
        public string UrlAvt { get; set; }
    }
}