using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;

namespace MoriiCoffee.Controllers
{
    public class ThucDonController : Controller
    {
        // GET: ThucDon
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private SanPham sp = new SanPham();
        private SanPhamDao spdao = new SanPhamDao();
        private NguoiDungDao nguoidungdao = new NguoiDungDao();
        private ChiTietSanPham ctsp = new ChiTietSanPham();
        private ChiTietSanPhamDao ctspdao = new ChiTietSanPhamDao();
        public ActionResult Index()
        {
            ViewBag.sanphams = spdao.ViewAll();
            ViewBag.ctsps = ctspdao.ViewAll();
            return View();
        }
        
    }
}