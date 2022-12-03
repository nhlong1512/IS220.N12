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

        [Required(ErrorMessage = "Vui lòng nhập Họ tên. ")]
        [StringLength(250)]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email. ")]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage ="Email không hợp lệ. ")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email không hợp lệ. ")]
        [StringLength(250)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu. ")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage ="Mật khẩu không hợp lệ. ")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,24}$", ErrorMessage = "Mật khẩu không hợp lệ. ")]
        [StringLength(32/*, MinimumLength = 8, ErrorMessage ="Độ dài mật khẩu ít nhất 8 kí tự. "*/)]
        public string Password { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Vui lòng xác nhận Mật khẩu. ")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không hợp lệ. ")]
        public string ConfirmPassword { get; set; }


        [StringLength(250)]
        public string Role { get; set; }

        [StringLength(50)]
        public string SDT { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        //[Required(ErrorMessage = "Vui lòng chọn ngày sinh. ")]
        public DateTime? NgSinh { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        //[Required(ErrorMessage = "Vui lòng chọn giới tính. ")]
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