namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DatHang
    {
        [Key]
        public int MaDH { get; set; }

        public int MaHD { get; set; }

        [Required]
        public string TrangThai { get; set; }

        [Required]
        public string DiaChiNH { get; set; }
    }
}
