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
               name: "QuenMatKhau",
               url: "quen-mat-khau",
               defaults: new { controller = "User", action = "QuenMatKhau", id = UrlParameter.Optional }
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


            //Route mặc định luôn phải để ở cuối cùng, tránh ghi đè lên các route đã được customize

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "TrangChu", action = "TrangChu", id = UrlParameter.Optional }
            );


        }
    }
}
