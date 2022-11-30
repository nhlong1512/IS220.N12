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
    public class DonDatController : Controller
    {
        // GET: Admin/DonDat
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private NguoiDungDao nguoidungdao = new NguoiDungDao();
        private DatHangDao dhdao = new DatHangDao();
        private HoaDonDao hddao = new HoaDonDao();
        private ChiTietHoaDonDao cthddao = new ChiTietHoaDonDao();
        private ChiTietSanPhamDao ctspdao = new ChiTietSanPhamDao();
        public ActionResult DanhSachDonDat()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }

            ViewBag.dathangs = dhdao.ViewAll();
            ViewBag.hoadons = hddao.ViewAll();
            ViewBag.nds = nguoidungdao.ViewAll();
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ChiTietDonDat(long id)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
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