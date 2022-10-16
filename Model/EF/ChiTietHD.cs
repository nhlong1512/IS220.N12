namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ChiTietHD
    {
        [Key]
        public int MaHD { get; set; }

        public int MaSP { get; set; }

        public int SoLuong { get; set; }

        public int ThanhTien { get; set; }
    }
}
