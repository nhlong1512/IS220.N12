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

namespace MoriiCoffee.Areas.Admin.Controllers
{
    public class AdminProfileController : Controller
    {
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private NguoiDungDao nddao = new NguoiDungDao();
        private DatHangDao dhdao = new DatHangDao();
        private HoaDonDao hddao = new HoaDonDao();
        private ChiTietHoaDonDao cthddao = new ChiTietHoaDonDao();
        private ChiTietSanPhamDao ctspdao = new ChiTietSanPhamDao();
        // GET: Admin/Profile
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult Update(long id)
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
            var ndd = nddao.ViewDetail(id);
            if (ndd.GioiTinh == null)
            {
                ndd.GioiTinh = true;
            }
            return View(ndd);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(NguoiDung nd)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                ViewBag.session = session;
                var nddd = nddao.ViewDetailEmail(session.UserName);
                if (nddd.Role != "Nhân viên" && nddd.Role != "ADMIN")
                {
                    return Redirect("/");
                }
                ViewBag.ndd = nddd;
            }

            var isTrue = nddao.Update(nd);
            if (isTrue)
            {
                var msg = "Cập nhật thông tin thành công. ";
                ViewBag.msg = msg;
                return Redirect("/admin/profile/chinh-sua/"+nd.ID);
            }
            else
            {
                ModelState.AddModelError("", "Không lưu được vào CSDL");
                return Redirect("/admin/profile/chinh-sua/" + nd.ID);
            }

        }



        [ValidateInput(false)]
        public ActionResult DoiMatKhau(long id)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                ViewBag.session = session;
                var nddd = nddao.ViewDetailEmail(session.UserName);
                if (nddd.Role != "Nhân viên" && nddd.Role != "ADMIN")
                {
                    return Redirect("/");
                }
                ViewBag.ndd = nddd;
            }
            var nd = nddao.ViewDetail(id);
            return View(nd);
        }


        [HttpPost]
        public JsonResult DoiMatKhau(long id, string oldPassword, string newPassword, string confirmNewPassword)
        {
            
            var err = "";
            var nd = nddao.ViewDetail(id);
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
    }
}