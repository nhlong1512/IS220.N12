namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HoaDon
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
