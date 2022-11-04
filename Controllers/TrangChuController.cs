using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MoriiCoffee.Controllers
{
    public class TrangChuController : Controller
    {
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private SanPham sp = new SanPham();
        private SanPhamDao spdao = new SanPhamDao();
        private NguoiDungDao nguoidungdao = new NguoiDungDao();
        private ChiTietSanPham ctsp = new ChiTietSanPham();
        private ChiTietSanPhamDao ctspdao = new ChiTietSanPhamDao();
        // GET: TrangChu
        public ActionResult TrangChu()
        {
            ViewBag.sanphams = spdao.ViewAll();
            ViewBag.ctsps = ctspdao.ViewAll();
            return View();
        }
        public ActionResult VeChungToi()
        {
            return View();
        }

        public ActionResult ThucDon()
        {
            return View();
        }
        public ActionResult Blog()
        {
            ViewBag.LongNguyen = db.Blogs.Count();
            return View();
        }
        public ActionResult CuaHang()
        {
            return View();
        }
        public ActionResult LienHe()
        {
            return View();
        }
    }
}