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

        public ActionResult DanhSachKhachHang()
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
            return View();
        }
    }
}