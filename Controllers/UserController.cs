using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static System.Collections.Specialized.BitVector32;
using System.Data.Entity.Migrations;
using Model.EF;
using MoriiCoffee.Common;
using Model.Dao;
using Facebook;
using System.Configuration;
using System.Web.Routing;

namespace MoriiCoffee.Controllers
{
    public class UserController : Controller
    {
        private MoriiCoffeeDBContext _db = new MoriiCoffeeDBContext();
        private NguoiDungDao nddao = new NguoiDungDao();
        private NguoiDung nd = new NguoiDung();
        private const string CartSession = "CartSession";
        public static string randomValidationCode;
        public static string emailForgotPassword;
        public static string msgSuccessSend;

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        // GET: User
        public ActionResult DangKy()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(NguoiDung user)
        {
            var err = "";
            if (ModelState.IsValid)
            {
                //var nd = _db.NguoiDungs.FirstOrDefault(s => s.Email == _user.Email);
                var nd = nddao.ViewDetailEmail(user.Email);


                if (nd == null)
                {
                    user.Password = GetMD5(user.Password);
                    user.ConfirmPassword = user.ConfirmPassword;
                    user.Status = true;
                    user.Role = "Khách hàng";
                    var id = nddao.Insert(user);
                    if (id >= 0)
                    {
                        return RedirectToAction("DangNhap");

                    }
                    else
                    {
                        err += "Thêm thất bại. ";
                    }
                }
                else
                {
                    err += "Email đã tồn tại. ";
                    ViewBag.err = err;
                    return View();
                }
            }
            else
            {
                var list = ModelState.ToDictionary(x => x.Key, y => y.Value.Errors.Select(x => x.ErrorMessage).ToArray())
                  .Where(m => m.Value.Count() > 0);
                foreach (var itm in list)
                {
                    err += string.Concat(string.Join(",", itm.Value.ToArray()), "");
                }
                ViewBag.err = err;
                return View();
            }



            return View();


        }

        public ActionResult DangNhap()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(string email, string password)
        {
            var err = "";

            if (email == "" || password == "")
            {
                if (email == "")
                {
                    err += "Vui lòng nhập Email. ";
                }
                if (password == "")
                {
                    err += "Vui lòng nhập Mật khẩu. ";
                }
                ViewBag.err = err;
                return View();
            }

            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = _db.NguoiDungs.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                var nddao = new NguoiDungDao();

                if (data.Count() > 0)
                {
                    //LongCode
                    var nd = nddao.ViewDetailEmail(email);
                    var userSession = new UserLogin();
                    userSession.UserName = nd.Email;
                    userSession.UserID = nd.ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    if(nd.Role == "ADMIN" || nd.Role == "Nhân viên")
                    {
                        return Redirect("/admin");
                    }
                    return Redirect("/");
                }
                else
                {
                    err += "Tài khoản hoặc mật khẩu không chính xác. ";
                    ViewBag.err = err;
                    return View("DangNhap");
                }
            }
            else
            {
                var list = ModelState.ToDictionary(x => x.Key, y => y.Value.Errors.Select(x => x.ErrorMessage).ToArray())
                  .Where(m => m.Value.Count() > 0);
                foreach (var itm in list)
                {
                    err += string.Concat(string.Join(",", itm.Value.ToArray()), "");
                }
                ViewBag.err = err;
                return View();
            }

        }

        //Login Facebook
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email,birthday,picture,gender");
                string email = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;
                string birthday = me.birthday;
                string urlImage = me.picture.data.url;
                //string phone = me.mobile_phone;
                string gender = me.gender;
                var user = new NguoiDung();
                user.Email = email;
                user.Status = true;
                user.HoTen = lastname + " " + middlename + " " + firstname;
                user.CreatedDate = DateTime.Now;
                user.Password = "FaceBookLogin2022!";
                user.ConfirmPassword = "FaceBookLogin2022!";
                user.Role = "Khách hàng";
                user.Urlmage = urlImage;
                //user.SDT = phone;
                if (gender == "male")
                {
                    user.GioiTinh = true;
                }
                if (gender == "female")
                {
                    user.GioiTinh = false;
                }
                //user.NgSinh = DateTime.Parse(birthday);
                var resultInsert = nddao.InsertForFacebook(user);
                if (resultInsert > 0)
                {
                    var userSession = new UserLogin();
                    userSession.UserName = user.Email;
                    userSession.UserID = user.ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return Redirect("/");
                }

            }
            return Redirect("/");
        }


        [HttpPost]
        public JsonResult LoginGoogleAjax(string accessToken, string displayName, string email, string phoneNumber, string photoUrl)
        {
            if (!string.IsNullOrEmpty(accessToken))
            {
                var user = new NguoiDung();
                user.Email = email;
                user.Status = true;
                user.HoTen = displayName;
                user.CreatedDate = DateTime.Now;
                user.Password = "GoogleLogin2022!";
                user.ConfirmPassword = "GoogleLogin2022!";
                user.Role = "Khách hàng";
                user.Urlmage = photoUrl;
                user.SDT = phoneNumber;
                var resultInsert = nddao.InsertForGoogle(user);
                if (resultInsert > 0)
                {
                    var userSession = new UserLogin();
                    userSession.UserName = user.Email;
                    userSession.UserID = user.ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                }
            }


            return Json(new
            {
                status = true,
                accescToken = accessToken,
                displayName = displayName,
                email = email,
                phoneNumber = phoneNumber,
                photoUrl = photoUrl,
            });
        }


        public ActionResult DangXuat()
        {
            //LongCode
            Session[CommonConstants.USER_SESSION] = null;
            Session["CartSession"] = null;
            return Redirect("/");

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


        public ActionResult QuenMatKhau()
        {
            return View();
        }

        [HttpPost]
        public ActionResult QuenMatKhau(string Email)
        {
            var user = _db.NguoiDungs.FirstOrDefault(n => n.Email == Email);
            if (user != null)
            {
                var mail = new SmtpClient("smtp.gmail.com", 587)
                {
                    //moriicoffeee@gmail.com@!!
                    Credentials = new NetworkCredential("moriicoffeee@gmail.com", "sasdyaredyhjonoo"),
                    EnableSsl = true,
                };

                var message = new MailMessage();
                message.From = new MailAddress("moriicoffee@gmail.com");
                //message.ReplyToList.Add("trantrongtin01012002@gmail.com");
                message.To.Add(new MailAddress(user.Email));
                message.Subject = "XÁC MINH MẬT KHẨU";

                //random 6 digits
                Random r = new Random();
                var x = r.Next(0, 1000000);
                string s = x.ToString("000000");
                randomValidationCode = s;
                message.Body = "Mã xác minh của bạn là: " + s;

                mail.Send(message);
                emailForgotPassword = Email;

                return RedirectToAction("ValidationCode", "User");
            }
            //Email không có trong DB
            else
            {
                var err = "Email người dùng không tồn tại. ";
                ViewBag.err = err;
                return View();
            }

        }

        //[HttpPost]
        public ActionResult GuiLaiMaXacMinh()
        {
            var Email = emailForgotPassword;
            var mail = new SmtpClient("smtp.gmail.com", 587)
            {
                //moriicoffeee@gmail.com@!!
                Credentials = new NetworkCredential("moriicoffeee@gmail.com", "sasdyaredyhjonoo"),
                EnableSsl = true,
            };

            var message = new MailMessage();
            message.From = new MailAddress("moriicoffee@gmail.com");
            message.To.Add(new MailAddress(Email));
            message.Subject = "XÁC MINH MẬT KHẨU";

            //random 6 digits
            Random r = new Random();
            var x = r.Next(0, 1000000);
            string s = x.ToString("000000");
            randomValidationCode = s;
            message.Body = "Mã xác minh của bạn là: " + s;

            mail.Send(message);
            var msg = "Chúng tôi đã gửi lại mã xác minh cho bạn. ";
            msgSuccessSend = msg;

            return RedirectToAction("ValidationCode", "User");

        }

        public ActionResult ValidationCode()
        {
            if (!string.IsNullOrEmpty(msgSuccessSend))
            {
                ViewBag.msg = msgSuccessSend;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidationCode(string code)
        {
            if (!string.IsNullOrEmpty(msgSuccessSend))
            {
                ViewBag.msg = msgSuccessSend;
            }
            if (code == randomValidationCode)
            {
                return RedirectToAction("ResetPassword", "User");
            }
            else
            {
                var err = "Mã xác minh không chính xác. ";
                ViewBag.err = err;
                return View();
            }
        }

        public ActionResult ResetPassword()
        {
            var nd = nddao.ViewDetailEmail(emailForgotPassword);
            ViewBag.nd = nd;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(NguoiDung user)
        {
            var err = "";
            var ndd = nddao.ViewDetailEmail(emailForgotPassword);
            if (ModelState.IsValid)
            {
                //var nd = _db.NguoiDungs.FirstOrDefault(s => s.Email == _user.Email);


                ndd.Password = GetMD5(user.Password);
                ndd.ConfirmPassword = GetMD5(user.ConfirmPassword);


                var nddd = _db.NguoiDungs.Find(ndd.ID);
                nddd.HoTen = ndd.HoTen;
                nddd.SDT = ndd.SDT;
                nddd.NgSinh = ndd.NgSinh;
                nddd.GioiTinh = ndd.GioiTinh;
                nddd.Password = ndd.Password;
                nddd.Status = ndd.Status;
                nddd.Urlmage = ndd.Urlmage;
                nddd.ModifiedBy = ndd.ModifiedBy;
                nddd.ModifiedDate = DateTime.Now;
                _db.Configuration.ValidateOnSaveEnabled = false;
                _db.SaveChanges();
                return Redirect("~/dang-nhap");

            }
            else
            {
                var list = ModelState.ToDictionary(x => x.Key, y => y.Value.Errors.Select(x => x.ErrorMessage).ToArray())
                  .Where(m => m.Value.Count() > 0);
                foreach (var itm in list)
                {
                    err += string.Concat(string.Join(",", itm.Value.ToArray()), "");
                }
                ViewBag.err = err;
                return View();
            }
            return View();
        }


    }
}