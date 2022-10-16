namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ChiTietSP
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
