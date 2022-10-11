using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoriiCoffee.Models
{
    [Table("Blogs")]
    public class Blog
    {
        [Key]
        public int MaBL { get; set; }
        [Required]
        public string TieuDe { get; set; } //Title
        [Required]
        public string MoTa { get; set; } //Description
        [Required]
        public string NoiDung { get; set; } //Content
        [Required]
        public string UrlImage { get; set; } 
        //Link của bài viết, nếu không có link sẽ trả về hình ảnh có sẵn là rỗng
        public int? MaND { get; set; } // Người đăng bài viết
        public DateTime NgayBlog { get; set; } //Ngày đăng bài viết

    }
}