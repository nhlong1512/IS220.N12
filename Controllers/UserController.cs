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

namespace MoriiCoffee.Controllers
{
    public class UserController : Controller
    {
        private MoriiCoffeeDBContext _db = new MoriiCoffeeDBContext();
        // GET: User
        public ActionResult DangKy()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(NguoiDung _user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.NguoiDungs.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.NguoiDungs.Add(_user);
                    _db.SaveChanges();
                    return RedirectToAction("DangNhap");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


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
            if (ModelState.IsValid)
            {

                //var f_password = GetMD5(password);
                //var data = _db.NguoiDungs.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                var nddao = new NguoiDungDao();
                var data = _db.NguoiDungs.Where(s => s.Email.Equals(email) && s.Password.Equals(password)).ToList();

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
                    ViewBag.error = "Login failed";
                    return RedirectToAction("DangNhap");
                }
            }
            return View();
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