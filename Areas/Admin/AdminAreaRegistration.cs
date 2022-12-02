using System.Web.Mvc;

namespace MoriiCoffee.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {


            context.MapRoute(
               name: "Dashboard",
               url: "admin/dashboard",
               defaults: new { controller = "Admin", action = "Dashboard", id = UrlParameter.Optional }
           );

            //Config Route cho Blog
            context.MapRoute(
               name: "Blog",
               url: "admin/blog",
               defaults: new { controller = "Blog", action = "DanhSachBlog", id = UrlParameter.Optional }
           );
            context.MapRoute(
               name: "BlogCreate",
               url: "admin/blog/create",
               defaults: new { controller = "Blog", action = "Create", id = UrlParameter.Optional }
           );



            //Config Route cho ChiTietSanPham
            context.MapRoute(
               name: "ChiTietSanPham",
               url: "admin/san-pham",
               defaults: new { controller = "ChiTietSanPham", action = "DanhSachSanPham", id = UrlParameter.Optional }
           );

            context.MapRoute(
               name: "DanhSachCaPhe",
               url: "admin/san-pham/ca-phe",
               defaults: new { controller = "ChiTietSanPham", action = "DanhSachCaPhe", id = UrlParameter.Optional }
           );

            context.MapRoute(
               name: "DanhSachTopping",
               url: "admin/san-pham/topping",
               defaults: new { controller = "ChiTietSanPham", action = "DanhSachTopping", id = UrlParameter.Optional }
           );

            context.MapRoute(
               name: "DanhSachSanPhamKhac",
               url: "admin/san-pham/khac",
               defaults: new { controller = "ChiTietSanPham", action = "DanhSachKhac", id = UrlParameter.Optional }
           );

            context.MapRoute(
               name: "DanhSachTraSua",
               url: "admin/san-pham/tra-sua",
               defaults: new { controller = "ChiTietSanPham", action = "DanhSachTraSua", id = UrlParameter.Optional }
           );


            context.MapRoute(
              name: "ChiTietSanPhamCreate",
              url: "admin/san-pham/create",
              defaults: new { controller = "ChiTietSanPham", action = "Create", id = UrlParameter.Optional }
          );

            context.MapRoute(
              name: "ChiTietSanPhamDetails",
              url: "admin/san-pham/details/{id}",
              defaults: new { controller = "ChiTietSanPham", action = "Details", id = UrlParameter.Optional }
          );
            context.MapRoute(
              name: "ChiTietSanPhamDelete",
              url: "admin/san-pham/delete/{id}",
              defaults: new { controller = "ChiTietSanPham", action = "Delete", id = UrlParameter.Optional }
          );
            context.MapRoute(
              name: "ChiTietSanPhamUpdate",
              url: "admin/san-pham/update/{id}",
              defaults: new { controller = "ChiTietSanPham", action = "Update", id = UrlParameter.Optional }
          );


            //Config Route cho Đơn Đặt
            context.MapRoute(
              name: "AdminDonDat",
              url: "admin/don-dat",
              defaults: new { controller = "DonDat", action = "DanhSachDonDat", id = UrlParameter.Optional }
          );

            context.MapRoute(
              name: "AdminDonDatChoXacNhan",
              url: "admin/don-dat/cho-xac-nhan",
              defaults: new { controller = "DonDat", action = "DanhSachChoXacNhan", id = UrlParameter.Optional }
          );

            context.MapRoute(
              name: "AdminDonDatDangGiao",
              url: "admin/don-dat/dang-giao",
              defaults: new { controller = "DonDat", action = "DanhSachDangGiao", id = UrlParameter.Optional }
          );

            context.MapRoute(
              name: "AdminDonDatDaGiao",
              url: "admin/don-dat/da-giao",
              defaults: new { controller = "DonDat", action = "DanhSachDaGiao", id = UrlParameter.Optional }
          );

            context.MapRoute(
              name: "AdminDonDatDaHuy",
              url: "admin/don-dat/da-huy",
              defaults: new { controller = "DonDat", action = "DanhSachDaHuy", id = UrlParameter.Optional }
          );

            context.MapRoute(
              name: "AdminChiTietDonDat",
              url: "admin/don-dat/chi-tiet-don-dat/{id}",
              defaults: new { controller = "DonDat", action = "ChiTietDonDat", id = UrlParameter.Optional }
          );


            //Config Route cho AdminProfile
            context.MapRoute(
              name: "AdminProfileTTCaNhan",
              url: "admin/profile/chinh-sua/{id}",
              defaults: new { controller = "AdminProfile", action = "Update", id = UrlParameter.Optional }
          );






            //Luôn để cái default này ở cuối, để ở trên sẽ lỗi
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new {Controller="Admin", action = "Dashboard", id = UrlParameter.Optional }
            );

            
        }
    }
}