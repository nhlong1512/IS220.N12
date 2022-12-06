using Model.Dao;
using Model.EF;
using MoriiCoffee.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MoriiCoffee.Controllers
{
    public class ProfileController : BaseController
    {
        // GET: Profile
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private NguoiDungDao nguoidungdao = new NguoiDungDao();
        private DatHangDao dhdao = new DatHangDao();
        private HoaDonDao hddao = new HoaDonDao();
        private ChiTietHoaDonDao cthddao = new ChiTietHoaDonDao();
        private ChiTietSanPhamDao ctspdao = new ChiTietSanPhamDao();
        private KhuyenMaiDao kmdao = new KhuyenMaiDao();
        private const string CartSession = "CartSession";

        public ActionResult Index()
        {
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
                    var ndd = nguoidungdao.ViewDetailEmail(session.UserName);
                    ViewBag.ndd = ndd;
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
            var nd = nguoidungdao.ViewDetail(id);
            if (nd.GioiTinh == null)
            {
                nd.GioiTinh = true;
            }

            return View(nd);

        }


        [ValidateInput(false)]
        public ActionResult Update(long id)
        {
            if (ModelState.IsValid)
            {
                var session = new UserLogin();
                session = (UserLogin)Session[CommonConstants.USER_SESSION];

                if (!(session is null))
                {
                    ViewBag.session = session;
                    var ndd = nguoidungdao.ViewDetailEmail(session.UserName);
                    ViewBag.ndd = ndd;
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
            var nd = nguoidungdao.ViewDetail(id);
            if (nd.GioiTinh == null)
            {
                nd.GioiTinh = true;
            }
            return View(nd);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(NguoiDung nd)
        {
            if (ModelState.IsValid)
            {
                var session = new UserLogin();
                session = (UserLogin)Session[CommonConstants.USER_SESSION];

                if (!(session is null))
                {
                    ViewBag.session = session;
                    var ndd = nguoidungdao.ViewDetailEmail(session.UserName);
                    ViewBag.ndd = ndd;
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

            var isTrue = nguoidungdao.Update(nd);
            if (isTrue)
            {
                var msg = "Cập nhật thông tin thành công. ";
                ViewBag.msg = msg;
                return RedirectToAction("Update", "Profile");
            }
            else
            {
                ModelState.AddModelError("", "Không lưu được vào CSDL");
                return RedirectToAction("Update", "Profile");
            }

        }


        [ValidateInput(false)]
        public ActionResult DoiMatKhau(long id)
        {
            if (ModelState.IsValid)
            {
                var session = new UserLogin();
                session = (UserLogin)Session[CommonConstants.USER_SESSION];

                if (!(session is null))
                {
                    ViewBag.session = session;
                    var ndd = nguoidungdao.ViewDetailEmail(session.UserName);
                    ViewBag.ndd = ndd;
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
            var nd = nguoidungdao.ViewDetail(id);
            //if (nd.GioiTinh == null)
            //{
            //    nd.GioiTinh = true;
            //}
            return View(nd);
        }


        [HttpPost]
        public JsonResult DoiMatKhau(long id, string oldPassword, string newPassword, string confirmNewPassword)
        {
            var session = new UserLogin();
            session = (UserLogin)Session[CommonConstants.USER_SESSION];

            if (!(session is null))
            {
                ViewBag.session = session;
                var ndd = nguoidungdao.ViewDetailEmail(session.UserName);
                ViewBag.ndd = ndd;
            }
            var cart = Session[CartSession];
            var listCart = new List<CartItem>();
            if (cart != null)
            {
                listCart = (List<CartItem>)cart;

                var cartQtySession = listCart.Count();
                ViewBag.cartQtySession = cartQtySession;
            }

            var err = "";
            var nd = nguoidungdao.ViewDetail(id);
            var isValid = true;

            //Check nếu không hợp lệ sẽ bỏ tất cả vào err
            if (!ModelState.IsValid)
            {
                var list = ModelState.ToDictionary(x => x.Key, y => y.Value.Errors.Select(x => x.ErrorMessage).ToArray())
                  .Where(m => m.Value.Count() > 0);
                foreach (var itm in list)
                {
                    err += string.Concat(string.Join(",", itm.Value.ToArray()), "");
                }
            }
            //Và check nếu hợp lệ
            if (ModelState.IsValid)
            {

                if (string.IsNullOrEmpty(oldPassword))
                {
                    isValid = false;
                    err += "Vui lòng nhập mật khẩu cũ. ";
                }
                else
                {
                    if (!(nd.Password == GetMD5(oldPassword)))
                    {
                        isValid = false;
                        err += "Mật khẩu cũ không chính xác. ";
                    }
                }
                
                if (newPassword == "")
                {
                    isValid = false;
                    err += "Vui lòng nhập mật khẩu mới. ";
                }
                else
                {

                string strRegex = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,24}$";
                Regex re = new Regex(strRegex);
                if (!re.IsMatch(newPassword))
                {
                    isValid = false;
                    err += "Mật khẩu mới không hợp lệ. ";
                }
                }

                if (confirmNewPassword == "")
                {
                    isValid = false;
                    err += "Vui lòng xác nhận mật khẩu mới. ";
                }
                else
                {

                    if (newPassword != confirmNewPassword)
                    {
                        isValid = false;
                        err += "Mật khẩu xác nhận không hợp lệ. ";
                    }
                }

                //If isValid. Update NewPassword
                if (isValid)
                {
                    nd.Password = GetMD5(newPassword);
                    nd.ConfirmPassword = GetMD5(confirmNewPassword);
                    var nddd = db.NguoiDungs.Find(nd.ID);
                    nddd.HoTen = nd.HoTen;
                    nddd.SDT = nd.SDT;
                    nddd.NgSinh = nd.NgSinh;
                    nddd.GioiTinh = nd.GioiTinh;
                    nddd.Password = nd.Password;
                    nddd.Status = nd.Status;
                    nddd.Urlmage = nd.Urlmage;
                    nddd.ModifiedBy = nd.ModifiedBy;
                    nddd.ModifiedDate = DateTime.Now;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();

                }

            }
            return Json(new
            {
                isValid = isValid,
                status = true,
                err = err
            });
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }



        [ValidateInput(false)]
        public ActionResult DonDat()
        {
            if (ModelState.IsValid)
            {
                var session = new UserLogin();
                session = (UserLogin)Session[CommonConstants.USER_SESSION];

                if (!(session is null))
                {
                    ViewBag.session = session;
                    var ndd = nguoidungdao.ViewDetailEmail(session.UserName);
                    ViewBag.ndd = ndd;
                    ViewBag.dathangs = dhdao.ViewAllByID(ndd.ID);
                    ViewBag.hoadons = hddao.ViewAllByID(ndd.ID);

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
            return View();
        }

        [ValidateInput(false)]
        public ActionResult DonDatChoXacNhan()
        {
            if (ModelState.IsValid)
            {
                var session = new UserLogin();
                session = (UserLogin)Session[CommonConstants.USER_SESSION];

                if (!(session is null))
                {
                    ViewBag.session = session;
                    var ndd = nguoidungdao.ViewDetailEmail(session.UserName);
                    ViewBag.ndd = ndd;
                    ViewBag.dathangs = dhdao.ViewAllByIDChoXacNhan(ndd.ID);
                    ViewBag.hoadons = hddao.ViewAllByID(ndd.ID);

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
            return View();
        }

        [ValidateInput(false)]
        public ActionResult DonDatDangGiao()
        {
            if (ModelState.IsValid)
            {
                var session = new UserLogin();
                session = (UserLogin)Session[CommonConstants.USER_SESSION];

                if (!(session is null))
                {
                    ViewBag.session = session;
                    var ndd = nguoidungdao.ViewDetailEmail(session.UserName);
                    ViewBag.ndd = ndd;
                    ViewBag.dathangs = dhdao.ViewAllByIDDangGiao(ndd.ID);
                    ViewBag.hoadons = hddao.ViewAllByID(ndd.ID);

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
            return View();
        }

        [ValidateInput(false)]
        public ActionResult DonDatDaGiao()
        {
            if (ModelState.IsValid)
            {
                var session = new UserLogin();
                session = (UserLogin)Session[CommonConstants.USER_SESSION];

                if (!(session is null))
                {
                    ViewBag.session = session;
                    var ndd = nguoidungdao.ViewDetailEmail(session.UserName);
                    ViewBag.ndd = ndd;
                    ViewBag.dathangs = dhdao.ViewAllByIDDaGiao(ndd.ID);
                    ViewBag.hoadons = hddao.ViewAllByID(ndd.ID);

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
            return View();
        }

        [ValidateInput(false)]
        public ActionResult DonDatDaHuy()
        {
            if (ModelState.IsValid)
            {
                var session = new UserLogin();
                session = (UserLogin)Session[CommonConstants.USER_SESSION];

                if (!(session is null))
                {
                    ViewBag.session = session;
                    var ndd = nguoidungdao.ViewDetailEmail(session.UserName);
                    ViewBag.ndd = ndd;
                    ViewBag.dathangs = dhdao.ViewAllByIDDaHuy(ndd.ID);
                    ViewBag.hoadons = hddao.ViewAllByID(ndd.ID);

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
            return View();
        }


        [ValidateInput(false)]
        public ActionResult ChiTietDonDat(long id)
        {
            var isKM = true;
            if (ModelState.IsValid)
            {
                var session = new UserLogin();
                session = (UserLogin)Session[CommonConstants.USER_SESSION];

                if (!(session is null))
                {
                    ViewBag.session = session;
                    var ndd = nguoidungdao.ViewDetailEmail(session.UserName);
                    ViewBag.ndd = ndd;
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
            var dh = dhdao.ViewDetail(id);
            var hd = hddao.ViewDetail(dh.MaHoaDon);
            var km = kmdao.ViewDetail(hd.MaKM);
            if (km.TenKM == "Không Khuyến Mãi" && km.ID == 1)
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
        public JsonResult HuyDonHangJson(long id)
        {
            var dh = dhdao.ViewDetail(id);
            var isValid = true;
            if(dh.TTDH == "Chờ Xác Nhận")
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
        public JsonResult DaNhanHangJson(long id)
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
    }
}





