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

            routes.MapRoute(
               name: "ResetPassword",
               url: "dat-lai-mat-khau",
               defaults: new { controller = "User", action = "ResetPassword", id = UrlParameter.Optional }
           );

            //Config Route cho UserProfile
            routes.MapRoute(
               name: "ProfileDetails",
               url: "profile/chi-tiet/{id}",
               defaults: new { controller = "Profile", action = "Details", id = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "ProfileUpdate",
              url: "profile/chinh-sua/{id}",
              defaults: new { controller = "Profile", action = "Update", id = UrlParameter.Optional }
          );

            routes.MapRoute(
             name: "ProfileDoiMatKhau",
             url: "profile/doi-mat-khau/{id}",
             defaults: new { controller = "Profile", action = "DoiMatKhau", id = UrlParameter.Optional }
         );

            routes.MapRoute(
            name: "ProfileDonDat",
            url: "profile/don-dat",
            defaults: new { controller = "Profile", action = "DonDat", id = UrlParameter.Optional }
        );

            routes.MapRoute(
            name: "ProfileDonDatChoXacNhan",
            url: "profile/don-dat/cho-xac-nhan",
            defaults: new { controller = "Profile", action = "DonDatChoXacNhan", id = UrlParameter.Optional }
        );

            routes.MapRoute(
            name: "ProfileDonDatDangGiao",
            url: "profile/don-dat/dang-giao",
            defaults: new { controller = "Profile", action = "DonDatDangGiao", id = UrlParameter.Optional }
        );

            routes.MapRoute(
            name: "ProfileDonDatDaGiao",
            url: "profile/don-dat/da-giao",
            defaults: new { controller = "Profile", action = "DonDatDaGiao", id = UrlParameter.Optional }
        );

            routes.MapRoute(
            name: "ProfileDonDatDaHuy",
            url: "profile/don-dat/da-huy",
            defaults: new { controller = "Profile", action = "DonDatDaHuy", id = UrlParameter.Optional }
        );

            routes.MapRoute(
           name: "ProfileChiTeitDonDat",
           url: "profile/chi-tiet-don-dat/{id}",
           defaults: new { controller = "Profile", action = "ChiTietDonDat", id = UrlParameter.Optional }
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
               name: "ThucDonTatCaClient",
               url: "thuc-don",
               defaults: new { controller = "ThucDon", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "ThucDonCaPhe",
               url: "thuc-don/ca-phe",
               defaults: new { controller = "ThucDon", action = "DanhSachCaPhe", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "ThucDonTraSua",
               url: "thuc-don/tra-sua",
               defaults: new { controller = "ThucDon", action = "DanhSachTraSua", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "ThucDonKhac",
               url: "thuc-don/khac",
               defaults: new { controller = "ThucDon", action = "DanhSachKhac", id = UrlParameter.Optional }
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
               defaults: new { controller = "BlogTrangChu", action = "DanhSachBlog", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "BlogTrangChuDetails",
               url: "blog/details/{id}",
               defaults: new { controller = "BlogTrangChu", action = "Details", id = UrlParameter.Optional }
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

            //Config Route cho Giỏ Hàng
            routes.MapRoute(
              name: "GioHang",
              url: "gio-hang",
              defaults: new { controller = "GioHang", action = "GioHang", id = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "ThemGioHang",
              url: "them-gio-hang",
              defaults: new { controller = "GioHang", action = "ThemGioHang", id = UrlParameter.Optional }
          );


            //Config Route cho Giao Hàng
            routes.MapRoute(
              name: "GiaoHang",
              url: "giao-hang",
              defaults: new { controller = "GiaoHang", action = "GiaoHang", id = UrlParameter.Optional }
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
