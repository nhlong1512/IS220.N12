namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CuaHang
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
