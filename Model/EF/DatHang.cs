namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DatHang")]
    public partial class DatHang
    {
        public long ID { get; set; }

        public long MaHoaDon { get; set; }

        public long? MaKH { get; set; }

        [StringLength(50)]
        public string HoTen { get; set; }

        [StringLength(50)]
        public string SDT { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(250)]
        public string DiaChiNhanHang { get; set; }

        [StringLength(50)]
        public string Tinh { get; set; }

        [StringLength(50)]
        public string Quan { get; set; }

        [StringLength(50)]
        public string Phuong { get; set; }

        [StringLength(250)]
        public string GhiChu { get; set; }

        [StringLength(50)]
        public string PTTT { get; set; }

        [StringLength(50)]
        public string TTDH { get; set; }

        public bool? Status { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        public string UrlImage { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
    }
}
