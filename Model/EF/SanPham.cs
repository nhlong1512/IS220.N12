namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SanPham
    {
        [Key]
        public int MaSP { get; set; }

        [Required]
        public string PhanLoai { get; set; }
    }
}
