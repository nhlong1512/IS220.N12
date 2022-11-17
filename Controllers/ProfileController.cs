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

            }

            var isTrue = nguoidungdao.Update(nd);
            if (isTrue)
            {
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


            var msg = "Thất bại";
            var err = "";
            var nd = nguoidungdao.ViewDetail(id);

            if (string.IsNullOrEmpty(oldPassword))
            {
                err += "Vui lòng nhập mật khẩu cũ. ";
            }

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
                //Check nếu mật khẩu cũ không chính xác
                if (!(nd.Password == GetMD5(oldPassword)))
                {
                    err += "Mật khẩu cũ không chính xác. ";
                }
                else
                {
                    var isValid = true;
                    if (newPassword == "")
                    {
                        err += "Vui lòng nhập mật khẩu mới. ";
                        isValid = false;
                    }
                    string strRegex = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,24}$";
                    Regex re = new Regex(strRegex);
                    if (!re.IsMatch(newPassword))
                    {
                        err += "Mật khẩu mới không hợp lệ. ";
                        isValid = false;
                    }

                    if (confirmNewPassword == "")
                    {
                        err += "Vui lòng xác nhận mật khẩu mới. ";
                        isValid = false;
                    }
                    if (newPassword != confirmNewPassword)
                    {
                        err += "Mật khẩu xác nhận không hợp lệ. ";
                        isValid = false;
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

                //else
                //{
                //    msg = "Thành công";
                //    return Json(new
                //    {
                //        status = true,
                //        msg = msg
                //    });
                //}
            }
            return Json(new
            {
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
        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult DoiMatKhau(NguoiDung nd)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var session = new UserLogin();
        //        session = (UserLogin)Session[CommonConstants.USER_SESSION];

        //        if (!(session is null))
        //        {
        //            ViewBag.session = session;
        //            var ndd = nguoidungdao.ViewDetailEmail(session.UserName);
        //            ViewBag.ndd = ndd;
        //        }

        //    }

        //    var isTrue = nguoidungdao.Update(nd);
        //    if (isTrue)
        //    {
        //        return RedirectToAction("DoiMatKhau", "Profile");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Không lưu được vào CSDL");
        //        return RedirectToAction("DoiMatKhau", "Profile");
        //    }

        //}
    }
}