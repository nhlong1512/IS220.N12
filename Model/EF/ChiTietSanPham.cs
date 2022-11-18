namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietSanPham")]
    public partial class ChiTietSanPham
    {
        public long ID { get; set; }

        [Required]
        [StringLength(250)]
        public string TenSanPham { get; set; }

        public decimal Gia { get; set; }

        public decimal? GiaCu { get; set; }

        public bool? Size { get; set; }

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

        [Column(TypeName = "ntext")]
        public string MoTaSanPham { get; set; }

        [Column("ChiTietSanPham", TypeName = "ntext")]
        public string ChiTietSanPham1 { get; set; }

        public long? MaPhanLoai { get; set; }
    }
}
