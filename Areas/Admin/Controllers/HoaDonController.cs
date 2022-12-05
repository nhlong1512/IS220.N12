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
    public class HoaDonController : Controller
    {
        // GET: Admin/HoaDon
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private NguoiDungDao nddao = new NguoiDungDao();
        private DatHangDao dhdao = new DatHangDao();
        private HoaDonDao hddao = new HoaDonDao();
        private ChiTietHoaDonDao cthddao = new ChiTietHoaDonDao();
        private ChiTietSanPhamDao ctspdao = new ChiTietSanPhamDao();

        public ActionResult DanhSachHoaDon(string searchString)
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
            var model = hddao.ListAllPaging(searchString);
            ViewBag.dathangs = model;
            ViewBag.hoadons = hddao.ViewAll();
            ViewBag.nds = nddao.ViewAll();
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ChiTietHoaDon(long id)
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
            var dh = dhdao.ViewDetail(id);
            var hd = hddao.ViewDetail(dh.MaHoaDon);
            var listCTHD = new List<ChiTietHoaDon>();
            listCTHD = cthddao.ViewAllByID(dh.MaHoaDon);
            var listCTSP = ctspdao.ViewAll();


            ViewBag.dh = dh;
            ViewBag.hd = hd;
            ViewBag.listCTHD = listCTHD;
            ViewBag.listCTSP = listCTSP;
            return View();
        }

    }
}