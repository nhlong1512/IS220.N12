﻿using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using MoriiCoffee.Controllers;
using MoriiCoffee.Common;

namespace MoriiCoffee.Areas.Admin.Controllers
{
    public class HoaDonController : Controller
    {
        // GET: Admin/HoaDon
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private NguoiDungDao nddao = new NguoiDungDao();
        private DatHangDao dhdao = new DatHangDao();
        private HoaDonDao hddao = new HoaDonDao();
        private ChiTietHoaDonDao cthddao = new ChiTietHoaDonDao();
        private ChiTietSanPhamDao ctspdao = new ChiTietSanPhamDao();
        private const string CartSession = "CartSession";

        public ActionResult DanhSachHoaDon(string searchString)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                ViewBag.session = session;
                var nd = nddao.ViewDetailEmail(session.UserName);
                ViewBag.ndd = nd;
            }
            var model = hddao.ListAllPaging(searchString);
            ViewBag.dathangs = model;
            ViewBag.hoadons = hddao.ViewAll();
            ViewBag.nds = nddao.ViewAll();
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ChiTietHoaDon(long id)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                ViewBag.session = session;
                var nd = nddao.ViewDetailEmail(session.UserName);
                ViewBag.ndd = nd;
            }
            var dh = dhdao.ViewDetail(id);
            var hd = hddao.ViewDetail(dh.MaHoaDon);
            var listCTHD = new List<ChiTietHoaDon>();
            listCTHD = cthddao.ViewAllByID(dh.MaHoaDon);
            var listCTSP = ctspdao.ViewAll();


            ViewBag.dh = dh;
            ViewBag.hd = hd;
            ViewBag.listCTHD = listCTHD;
            ViewBag.listCTSP = listCTSP;
            return View();
        }



        //Handle phương thức đặt giao trong phần giao hàng, khi ấn đặt giao và không xảy ra ngoại lệ nào
        //Các dữ liệu sẽ được đổ hết vào 3 bảng ChiTietHoaDon, HoaDon và Bảng DatHang

        [HttpPost]
        public JsonResult ThanhToanHoaDonJson(long id)
        {

            var cart = Session[CartSession];
            var list = (List<CartItem>)cart;
            var isValid = true;
            var errMsg = "";
            
            //Nếu tất cả các case đều hợp lệ, Ta sẽ tiến hành cập nhật dữ liệu và CSDL
            if (isValid == true)
            {
                //Thêm hóa đơn mới
                HoaDon hd = new HoaDon();
                hd.TongTien = 0;
                hd.IsOnline = true;
                hd.MaNV = id;
                var idhd = hddao.Insert(hd);
                //Kiểm tra nếu thêm được thì tiếp tục thêm dữ liệu cho bảng CTHD ngược lại thì không làm gì cả
                if (idhd > 0)
                {
                    foreach (var item in list)
                    {
                        //Thêm các sản phẩm đặt vào chi tiết hóa đơn
                        ChiTietHoaDon cthd = new ChiTietHoaDon();
                        cthd.MaSP = item.ChiTietSanPham.ID;
                        cthd.Size = item.Size;
                        cthd.Topping = item.Topping;
                        cthd.Gia = item.Gia;
                        cthd.SoLuong = item.Quantity;
                        cthd.ThanhTien = item.Quantity * item.Gia;
                        cthd.IDHoaDon = idhd;
                        var idcthd = cthddao.Insert(cthd);
                        if (idcthd <= 0)
                        {
                            isValid = false;
                        }
                        hd.TongTien += cthd.ThanhTien;
                    }

                    //Sau khi thêm tất cả các sản phẩm đặt vào chi tiết hóa đơn, ta tiến hành cập nhật tổng tiền
                    if (hd.TongTien > 0)
                    {
                        //Thêm 30 nghìn phí ship
                        hddao.UpdateTongTien(hd);

                    }
                   
                }
                else
                {
                    isValid = false;
                }
            }

            //Kiểm tra nếu tất cả đã hoàn thành và hợp lệ rồi thì sẽ đưa session về null
            if (isValid == true)
            {
                Session["CartSession"] = null;
            }


            return Json(new
            {
                isValid = isValid,
                listCart = list,
                id = id,
                errMsg = errMsg,

            });
        }

    }
}