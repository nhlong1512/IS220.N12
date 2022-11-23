using Model.Dao;
using Model.EF;
using MoriiCoffee.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoriiCoffee.Controllers
{
    public class CuaHangController : Controller
    {
        // GET: CuaHang
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private SanPham sp = new SanPham();
        private SanPhamDao spdao = new SanPhamDao();
        private NguoiDungDao nguoidungdao = new NguoiDungDao();
        private ChiTietSanPham ctsp = new ChiTietSanPham();
        private ChiTietSanPhamDao ctspdao = new ChiTietSanPhamDao();
        private const string CartSession = "CartSession";

        public ActionResult Index()
        {
            if (ModelState.IsValid)
            {
                var session = new UserLogin();
                session = (UserLogin)Session[CommonConstants.USER_SESSION];

                if (!(session is null))
                {
                    ViewBag.session = session;
                    var nd = nguoidungdao.ViewDetailEmail(session.UserName);
                    ViewBag.ndd = nd;
                }
                var cart = Session[CartSession];
                var list = new List<CartItem>();
                if (cart != null)
                {
                    list = (List<CartItem>)cart;

                    var cartQtySession = list.Count();
                    ViewBag.cartQtySession = cartQtySession;
                }


                ViewBag.sanphams = spdao.ViewAll();
                ViewBag.ctsps = ctspdao.ViewAll();
            }


            return View();
        }
    }
}