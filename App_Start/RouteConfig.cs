using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MoriiCoffee
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //Config route cho UserController
            routes.MapRoute(
                name: "DangNhap",
                url: "dang-nhap",
                defaults: new { controller = "User", action = "DangNhap", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DangKy",
                url: "dang-ky",
                defaults: new { controller = "User", action = "DangKy", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DangXuat",
                url: "dang-xuat",
                defaults: new { controller = "User", action = "DangXuat", id = UrlParameter.Optional }
            );


            routes.MapRoute(
               name: "QuenMatKhau",
               url: "quen-mat-khau",
               defaults: new { controller = "User", action = "QuenMatKhau", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "ValidationCode",
               url: "xac-minh",
               defaults: new { controller = "User", action = "ValidationCode", id = UrlParameter.Optional }
           );



            //Config route cho Trang chủ
            routes.MapRoute(
               name: "TrangChu",
               url: "",
               defaults: new { controller = "TrangChu", action = "Index", id = UrlParameter.Optional }
           );

            //Config route cho Câu chuyện
            routes.MapRoute(
               name: "CauChuyen",
               url: "cau-chuyen",
               defaults: new { controller = "CauChuyen", action = "Index", id = UrlParameter.Optional }
           );

            //Config Route cho thực đơn
            routes.MapRoute(
               name: "ThucDon",
               url: "thuc-don",
               defaults: new { controller = "ThucDon", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "ThucDonDetails",
               url: "thuc-don/details/{id}",
               defaults: new { controller = "ThucDon", action = "Details", id = UrlParameter.Optional }
           );

            //Config Route cho Blog 
            routes.MapRoute(
               name: "BlogTrangChu",
               url: "blog",
               defaults: new { controller = "BlogTrangChu", action = "Index", id = UrlParameter.Optional }
           );

            //Config Route cho Cửa hàng
            routes.MapRoute(
               name: "CuaHang",
               url: "cua-hang",
               defaults: new { controller = "CuaHang", action = "Index", id = UrlParameter.Optional }
           );

            //Config Route cho Liên Hệ 
            routes.MapRoute(
               name: "LienHe",
               url: "lien-he",
               defaults: new { controller = "LienHe", action = "Index", id = UrlParameter.Optional }
           );

            //Route mặc định luôn phải để ở cuối cùng, tránh ghi đè lên các route đã được customize

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "TrangChu", action = "TrangChu", id = UrlParameter.Optional }
            );


        }
    }
}
