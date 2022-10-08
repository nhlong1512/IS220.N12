using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoriiCoffee.Models;

namespace MoriiCoffee.Controllers
{
    public class TrangChuController : Controller
    {
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        // GET: TrangChu
        public ActionResult Index()
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