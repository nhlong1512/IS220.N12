
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;
using MoriiCoffee.Common;
using MoriiCoffee.Controllers;

namespace MoriiCoffee.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private Blog bl = new Blog();
        private BlogDao bldao = new BlogDao();
        private NguoiDungDao nddao = new NguoiDungDao();
        private ChiTietSanPhamDao ctspdao = new ChiTietSanPhamDao();
        private HoaDonDao hddao = new HoaDonDao();

        // GET: Admin/Admin
        public ActionResult Dashboard()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }else
            {
                ViewBag.session = session;
                var nd = nddao.ViewDetailEmail(session.UserName);
                ViewBag.ndd = nd;
            }
            var nds = nddao.ViewAll();
            ViewBag.nds = nds;
            ViewBag.ndsQty = nds.Count();
            ViewBag.ctspsQty = ctspdao.ViewAll().Count();
            var hds = hddao.ViewAll();
            ViewBag.tongTienHD = hds.Sum(p => p.TongTien);
            ViewBag.hdsQty = hds.Count();
            
            return View();
        }


    }
}