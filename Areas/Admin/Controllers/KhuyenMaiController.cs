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
    public class KhuyenMaiController : Controller
    {
        // GET: Admin/KhuyenMai
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private SanPham sp = new SanPham();
        private SanPhamDao spdao = new SanPhamDao();
        private NguoiDungDao nddao = new NguoiDungDao();
        private KhuyenMaiDao kmdao = new KhuyenMaiDao();

        public ActionResult DanhSachKhuyenMai()
        {
            var kms = kmdao.ViewAll();
            ViewBag.kms = kms;
            return View();
        }
    }
}