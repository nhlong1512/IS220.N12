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
        private NguoiDungDao nddao = new NguoiDungDao();
        private DatHangDao dhdao = new DatHangDao();
        private HoaDonDao hddao = new HoaDonDao();
        private ChiTietHoaDonDao cthddao = new ChiTietHoaDonDao();
        private ChiTietSanPhamDao ctspdao = new ChiTietSanPhamDao();
        private KhuyenMaiDao kmdao = new KhuyenMaiDao();

        public ActionResult DanhSachDonDat(string searchString)
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
                if (nd.Role != "Nhân viên" && nd.Role != "ADMIN")
                {
                    return Redirect("/");
                }
                ViewBag.ndd = nd;
            }
            var model = dhdao.ListAllPaging(searchString);
            ViewBag.dathangs = model;
            ViewBag.hoadons = hddao.ViewAll();
            ViewBag.nds = nddao.ViewAll();
            return View();
        }

        public ActionResult DanhSachChoXacNhan(string searchString)
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
                if (nd.Role != "Nhân viên" && nd.Role != "ADMIN")
                {
                    return Redirect("/");
                }
                ViewBag.ndd = nd;
            }
            var model = dhdao.ListAllPagingChoXacNhan(searchString);
            ViewBag.dathangs = model;
            ViewBag.hoadons = hddao.ViewAll();
            ViewBag.nds = nddao.ViewAll();
            return View();
        }

        public ActionResult DanhSachDangGiao(string searchString)
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
                if (nd.Role != "Nhân viên" && nd.Role != "ADMIN")
                {
                    return Redirect("/");
                }
                ViewBag.ndd = nd;
            }
            var model = dhdao.ListAllPagingDangGiao(searchString);
            ViewBag.dathangs = model;
            ViewBag.hoadons = hddao.ViewAll();
            ViewBag.nds = nddao.ViewAll();
            return View();
        }

        public ActionResult DanhSachDaGiao(string searchString)
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
                if (nd.Role != "Nhân viên" && nd.Role != "ADMIN")
                {
                    return Redirect("/");
                }
                ViewBag.ndd = nd;
            }
            var model = dhdao.ListAllPagingDaGiao(searchString);
            ViewBag.dathangs = model;
            ViewBag.hoadons = hddao.ViewAll();
            ViewBag.nds = nddao.ViewAll();
            return View();
        }
        public ActionResult DanhSachDaHuy(string searchString)
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
                if (nd.Role != "Nhân viên" && nd.Role != "ADMIN")
                {
                    return Redirect("/");
                }
                ViewBag.ndd = nd;
            }
            var model = dhdao.ListAllPagingDaHuy(searchString);
            ViewBag.dathangs = model;
            ViewBag.hoadons = hddao.ViewAll();
            ViewBag.nds = nddao.ViewAll();
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ChiTietDonDat(long id)
        {
            var isKM = true;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                ViewBag.session = session;
                var nd = nddao.ViewDetailEmail(session.UserName);
                if (nd.Role != "Nhân viên" && nd.Role != "ADMIN")
                {
                    return Redirect("/");
                }
                ViewBag.ndd = nd;
            }
            var dh = dhdao.ViewDetail(id);
            var hd = hddao.ViewDetail(dh.MaHoaDon);
            var km = kmdao.ViewDetail(hd.MaKM);
            if (km.TenKM == "Không Khuyến Mãi" || km.ID == 1)
            {
                isKM = false;
            }
            var listCTHD = new List<ChiTietHoaDon>();
            listCTHD = cthddao.ViewAllByID(dh.MaHoaDon);
            var listCTSP = ctspdao.ViewAll();

            ViewBag.km = km;
            ViewBag.isKM = isKM;
            ViewBag.dh = dh;
            ViewBag.hd = hd;
            ViewBag.listCTHD = listCTHD;
            ViewBag.listCTSP = listCTSP;
            return View();
        }


        //Handle Xác nhận Hủy Đơn hàng
        [HttpPost]
        public JsonResult HuyDonHangAdminJson(long id)
        {
            var dh = dhdao.ViewDetail(id);
            var isValid = true;
            if (dh.TTDH == "Chờ Xác Nhận")
            {
                dh.TTDH = "Đã Hủy";
            }
            else
            {
                isValid = false;
            }

            if (isValid == true)
            {
                dhdao.Update(dh);
            }
            return Json(new
            {
                status = true,
                id = id,
                isValid = isValid,
            });
        }

        //Handle Xác nhận Đã nhận hàng
        [HttpPost]
        public JsonResult DaNhanHangAdminJson(long id)
        {
            var dh = dhdao.ViewDetail(id);
            var isValid = true;
            if (dh.TTDH == "Đang Giao")
            {
                dh.TTDH = "Đã Giao";
            }
            else
            {
                isValid = false;
            }

            if (isValid == true)
            {
                dhdao.Update(dh);
            }
            return Json(new
            {
                status = true,
                id = id,
                isValid = isValid,
            });
        }

        //Handle Xác nhận Đang Giao Đơn Hàng
        [HttpPost]
        public JsonResult DangGiaoHangAdminJson(long id)
        {
            var dh = dhdao.ViewDetail(id);
            var isValid = true;
            if (dh.TTDH == "Chờ Xác Nhận")
            {
                dh.TTDH = "Đang Giao";
            }
            else
            {
                isValid = false;
            }

            if (isValid == true)
            {
                dhdao.Update(dh);
            }
            return Json(new
            {
                status = true,
                id = id,
                isValid = isValid,
            });
        }
    }
}