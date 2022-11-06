using Model.Dao;
using Model.EF;
using MoriiCoffee.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MoriiCoffee.Controllers
{
    public class TrangChuController : BaseController
    {
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private SanPham sp = new SanPham();
        private SanPhamDao spdao = new SanPhamDao();
        private NguoiDungDao nguoidungdao = new NguoiDungDao();
        private ChiTietSanPham ctsp = new ChiTietSanPham();
        private ChiTietSanPhamDao ctspdao = new ChiTietSanPhamDao();
        // GET: TrangChu
        public ActionResult Index()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    ViewBag.session = session;
                    var nd = nguoidungdao.ViewDetailEmail(session.UserName);
                    ViewBag.ndd = nd;
            
            ViewBag.sanphams = spdao.ViewAll();
            ViewBag.ctsps = ctspdao.ViewAll();
            return View();
        }
        
    }
}