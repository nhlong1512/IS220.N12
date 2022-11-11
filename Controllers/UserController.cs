﻿using System;
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

namespace MoriiCoffee.Controllers
{
    public class UserController : Controller
    {
        private MoriiCoffeeDBContext _db = new MoriiCoffeeDBContext();
        private NguoiDungDao nddao = new NguoiDungDao();
        private NguoiDung nd = new NguoiDung();

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
                    //TinCode
                    //add session
                    //Session["FullName"] = data.FirstOrDefault().HoTen;
                    //Session["Email"] = data.FirstOrDefault().Email;
                    //Session["idUser"] = data.FirstOrDefault().ID;
                    //return RedirectToAction("TrangChu", "TrangChu", new { area = "" });

                    //LongCode
                    var nd = nddao.ViewDetailEmail(email);
                    var userSession = new UserLogin();
                    userSession.UserName = nd.Email;
                    userSession.UserID = nd.ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "TrangChu");
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

        //public ActionResult LoginGoogle()
        //{
        //    var fb = new GoogleClient();
        //    var loginUrl = fb.GetLoginUrl(new
        //    {
        //        client_id = ConfigurationManager.AppSettings["FbAppId"],
        //        client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
        //        redirect_uri = RedirectUri.AbsoluteUri,
        //        response_type = "code",
        //        scope = "email",
        //    });
        //    return Redirect(loginUrl.AbsoluteUri);
        //}

        ////Login Google
        ////ClientID: 362865066330-vh0r90jvklie69u8q3daegoj9kmn5jn2.apps.googleusercontent.com
        ////ClientSecret: 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
        //public Action LoginGoogle()
        //{
        //    try
        //    {
        //        var url = Request.Url.Query;
        //        if (url != "")
        //        {
        //            string queryString = url.ToString();
        //            char[] delimiterChars = { '=' };
        //            string[] words = queryString.Split(delimiterChars);
        //            string code = words[1];
        //            if (code != null)
        //            {
        //                //get the access token
        //                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
        //                webRequest.Method = "POST";
        //                Parameters = "code=" + code + "&client_id=" + client_id + "&client_secret=" + client_sceret + "&redirect_uri=" + redirect_url + "&grant_type=authorization_code";
        //                byte[] byteArray = Encoding.UTF8.GetBytes(Parameters);
        //                webRequest.ContentType = "application/x-www-form-urlencoded";
        //                webRequest.ContentLength = byteArray.Length;
        //                Stream postStream = webRequest.GetRequestStream();
        //                // Add the post data to the web request
        //                postStream.Write(byteArray, 0, byteArray.Length);
        //                postStream.Close();
        //                WebResponse response = webRequest.GetResponse();
        //                postStream = response.GetResponseStream();
        //                StreamReader reader = new StreamReader(postStream);
        //                string responseFromServer = reader.ReadToEnd();
        //                GoogleAccessToken serStatus = JsonConvert.DeserializeObject<GoogleAccessToken>(responseFromServer);
        //                if (serStatus != null)
        //                {
        //                    string accessToken = string.Empty;
        //                    accessToken = serStatus.access_token;
        //                    Session["Token"] = accessToken;
        //                    if (!string.IsNullOrEmpty(accessToken))
        //                    {
        //                        //call get user information function with access token as parameter
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //}

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

        public ActionResult DangXuat()
        {
            //TinCode
            //Session.Clear();//remove session
            //return RedirectToAction("Login");


            //LongCode
            Session[CommonConstants.USER_SESSION] = null;
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
            string email1 = Email;
            var user = _db.NguoiDungs.FirstOrDefault(n => n.Email == email1);
            if (user != null)
            {
                ViewBag.ThongBao = "Đã gửi email thành công!, vui lòng kiểm tra lại email.";
                var mail = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("moriicoffeee@gmail.com", "cvxruiagevrkfnmz"),
                    EnableSsl = true,
                };

                var message = new MailMessage();
                message.From = new MailAddress("moriicoffeee@gmail.com");
                //message.ReplyToList.Add("trantrongtin01012002@gmail.com");
                message.To.Add(new MailAddress(user.Email));
                message.Subject = "Thông báo về thay đổi mk";
                string mk = "Abc12345678@";
                message.Body = "MK la: " + mk;
                user.Password = GetMD5(mk);
                _db.Configuration.ValidateOnSaveEnabled = false;
                _db.NguoiDungs.AddOrUpdate(user);
                _db.SaveChanges();
                mail.Send(message);

                return View("DangNhap");
            }
            else
            {
                return View("DangNhap");
            }

        }


    }
}