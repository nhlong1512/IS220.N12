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
using System.Security.Cryptography;
using System.Text;

namespace MoriiCoffee.Areas.Admin.Controllers
{
    public class NhanVienController : Controller
    {
        // GET: Admin/NhanVien
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private NguoiDungDao nddao = new NguoiDungDao();

        public ActionResult DanhSachNhanVien(string searchString)
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

            var model = nddao.ListAllPagingNhanVien(searchString);
            ViewBag.nds = model;
            return View();
        }

        public ActionResult Details(long id)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                ViewBag.session = session;
                var nddd = nddao.ViewDetailEmail(session.UserName);
                ViewBag.ndd = nddd;
            }

            var nd = nddao.ViewDetail(id);
            ViewBag.nd = nd;

            return View(nd);
        }


        // GET: User
        public ActionResult ThemNhanVien()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                ViewBag.session = session;
                var nddd = nddao.ViewDetailEmail(session.UserName);
                ViewBag.ndd = nddd;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemNhanVien(NguoiDung user)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                ViewBag.session = session;
                var nddd = nddao.ViewDetailEmail(session.UserName);
                ViewBag.ndd = nddd;
            }

            var err = "";
            if (ModelState.IsValid)
            {
                //var nd = _db.NguoiDungs.FirstOrDefault(s => s.Email == _user.Email);
                var nd = nddao.ViewDetailEmail(user.Email);
                if (nd == null)
                {
                    user.Password = GetMD5(user.Password);
                    user.ConfirmPassword = user.ConfirmPassword;
                    user.Status = true;
                    user.Role = "Khách hàng";
                    var id = nddao.Insert(user);
                    if (id >= 0)
                    {
                        return Redirect("/admin/nhan-vien");

                    }
                    else
                    {
                        err += "Thêm thất bại. ";
                    }
                }
                else
                {
                    err += "Email đã tồn tại. ";
                    ViewBag.err = err;
                    return View();
                }
            }
            else
            {
                var list = ModelState.ToDictionary(x => x.Key, y => y.Value.Errors.Select(x => x.ErrorMessage).ToArray())
                  .Where(m => m.Value.Count() > 0);
                foreach (var itm in list)
                {
                    err += string.Concat(string.Join(",", itm.Value.ToArray()), "");
                }
                ViewBag.err = err;
                return View();
            }
            return View();
        }


        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}