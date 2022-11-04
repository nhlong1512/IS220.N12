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

            //Cấu hình định tuyến cho Admin/Blog
            context.MapRoute(
               name: "Blog",
               url: "admin/blog",
               defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
           );

            context.MapRoute(
               name: "BlogCreate",
               url: "admin/blog/create",
               defaults: new { controller = "Blog", action = "Create", id = UrlParameter.Optional }
           );

            //Cấu hình định tuyến cho Sản phẩm
            context.MapRoute(
               name: "ChiTietSanPham",
               url: "admin/san-pham",
               defaults: new { controller = "ChiTietSanPham", action = "Index", id = UrlParameter.Optional }
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




            //Luôn để cái default này ở cuối, để ở trên sẽ lỗi
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new {Controller="Admin", action = "Dashboard", id = UrlParameter.Optional }
            );

            
        }
    }
}