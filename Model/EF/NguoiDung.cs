namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string HoTen { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(32)]
        public string Password { get; set; }

        [StringLength(250)]
        public string Role { get; set; }

        [StringLength(50)]
        public string SDT { get; set; }

        public DateTime? NgSinh { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        public bool? GioiTinh { get; set; }

        [StringLength(250)]
        public string Urlmage { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool? Status { get; set; }
    }
}
