﻿using Model.Dao;
using Model.EF;
using MoriiCoffee.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MoriiCoffee.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private NguoiDungDao nddao = new NguoiDungDao();
        private const string CartSession = "CartSession"; 

        public ActionResult GioHang()
        {
            if (ModelState.IsValid)
            {
                var session = new UserLogin();
                session = (UserLogin)Session[CommonConstants.USER_SESSION];

                if (!(session is null))
                {
                    ViewBag.session = session;
                    var ndd = nddao.ViewDetailEmail(session.UserName);
                    ViewBag.ndd = ndd;
                }
            }

            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if(cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public ActionResult ThemGioHang(long productID, int quantity)
        {
            var sanpham = new ChiTietSanPhamDao().ViewDetail(productID);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.ChiTietSanPham.ID == productID))
                {
                    foreach (var item in list)
                    {
                        if (item.ChiTietSanPham.ID == productID)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.ChiTietSanPham = sanpham;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                //Tạo mới đối tượng CartItem
                var item = new CartItem();
                item.ChiTietSanPham = sanpham;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào Session
                Session[CartSession] = list;
            }

            return Redirect("/gio-hang");
        }

        public ActionResult GiaoHang()
        {
            if (ModelState.IsValid)
            {
                var session = new UserLogin();
                session = (UserLogin)Session[CommonConstants.USER_SESSION];

                if (!(session is null))
                {
                    ViewBag.session = session;
                    var ndd = nddao.ViewDetailEmail(session.UserName);
                    ViewBag.ndd = ndd;
                }

            }
            return View();
        }
        


        
    }
}