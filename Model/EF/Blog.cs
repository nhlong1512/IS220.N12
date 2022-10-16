namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Blog
    {
        [Key]
        public int MaBL { get; set; }

        [Required]
        public string TieuDe { get; set; }

        [Required]
        public string MoTa { get; set; }

        [Required]
        public string NoiDung { get; set; }

        [Required]
        public string UrlImage { get; set; }

        public int? MaND { get; set; }

        public DateTime NgayBlog { get; set; }
    }
}
