﻿using System.Web.Mvc;

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
               defaults: new { controller = "Blog", action = "DanhSachBlog", id = UrlParameter.Optional }
           );
            context.MapRoute(
               name: "BlogCreate",
               url: "admin/blog/create",
               defaults: new { controller = "Blog", action = "Create", id = UrlParameter.Optional }
           );



            //Cấu hình định tuyến cho Sản Phẩm
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





            //Luôn để cái default này ở cuối, để ở trên sẽ lỗi
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new {Controller="Admin", action = "Dashboard", id = UrlParameter.Optional }
            );

            
        }
    }
}