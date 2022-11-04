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

        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        [StringLength(250)]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        [StringLength(32)]
        public string Password { get; set; }
        [NotMapped]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }


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