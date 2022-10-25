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
        // GET: TrangChu
        public ActionResult TrangChu()
        {
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