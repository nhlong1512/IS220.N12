namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHoaDon")]
    public partial class ChiTietHoaDon
    {
        public long ID { get; set; }

        public long? MaSP { get; set; }

        [StringLength(2)]
        public string Size { get; set; }

        [StringLength(50)]
        public string Topping { get; set; }

        public decimal? Gia { get; set; }

        public int? SoLuong { get; set; }

        public decimal? ThanhTien { get; set; }

        public long? IDHoaDon { get; set; }

        public bool? Status { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
    }
}
