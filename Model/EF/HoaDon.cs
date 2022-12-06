namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        public long ID { get; set; }

        public long? MaNV { get; set; }

        public long? MaKH { get; set; }

        public long? MaCH { get; set; }

        public decimal? TongTien { get; set; }

        public bool? IsOnline { get; set; }

        public long MaKM { get; set; }

        public decimal? TienKM { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool? Status { get; set; }
    }
}
