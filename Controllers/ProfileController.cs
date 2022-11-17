using Model.Dao;
using Model.EF;
using MoriiCoffee.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            if(nd.GioiTinh == null)
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
        public JsonResult QuenMatKhau(long id, string oldPassword, string newPassword, string confirmNewPassword)
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
            var msg = "Thất bại";
            var nd = nguoidungdao.ViewDetail(id);
            if(nd.Password == GetMD5(oldPassword))
            {
                msg = "Thành công";
            }
            return Json(new
            {
                status = true,
                msg = msg
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