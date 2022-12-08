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
    public class ThucDonController : Controller
    {
        // GET: ThucDon
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
                ViewBag.ctsps = ctspdao.ViewAllTatCaClient();
            }


            return View();
        }

        public ActionResult DanhSachCaPhe()
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
                ViewBag.ctsps = ctspdao.ViewAllCaPhe();
            }


            return View();
        }

        public ActionResult DanhSachTraSua()
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
                ViewBag.ctsps = ctspdao.ViewAllTraSua();
            }


            return View();
        }

        public ActionResult DanhSachKhac()
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
                ViewBag.ctsps = ctspdao.ViewAllKhac();
            }


            return View();
        }

        public ActionResult Details(long id)
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

            }

            var listTopping = ctspdao.ViewListTopping();
            ViewBag.listTopping = listTopping;

            var ctsp = ctspdao.ViewDetail(id);
            ViewBag.ctsps = ctspdao.ViewAll();

            return View(ctsp);
        }

    }
}