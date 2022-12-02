using Model.Dao;
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
    public class KhachHangController : Controller
    {
        // GET: Admin/KhachHang
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private NguoiDungDao nddao = new NguoiDungDao();

        public ActionResult DanhSachKhachHang(string searchString)
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

            var model = nddao.ListAllPagingKhachHang(searchString);
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

        [ValidateInput(false)]
        public ActionResult Create(NguoiDung nguoidung)
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

            if (ModelState.IsValid)
            {
                nguoidung.Status = true;
                var id = nddao.Insert(nguoidung);

                if (id > 0)
                {
                    return Redirect("/admin/khach-hang");
                }
                return View("Create");

            }
            else
            {

            }
            return View("Create");
        }


    }
}