namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NguoiDung
    {
        public int ID { get; set; }

        [Required]
        public string HoTen { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string SDT { get; set; }

        public string Role { get; set; }

        public DateTime? NgDK { get; set; }

        public DateTime? NgSinh { get; set; }

        public string GioiTinh { get; set; }

        public string UrlAvt { get; set; }
    }
}
