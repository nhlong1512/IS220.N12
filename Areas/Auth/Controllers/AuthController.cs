using MoriiCoffee.Models;
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

namespace MoriiCoffee.Areas.Auth.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth/Auth
        private MoriiCoffeeDBContext _db = new MoriiCoffeeDBContext();
        // GET: TrangChu
        public ActionResult Index()
        {

            return View("TrangChu/Index");


        }
        //GET: Register

        public ActionResult Register()
        {
            return View();
        }
        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(NguoiDung _user)
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
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();


        }
        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = _db.NguoiDungs.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().HoTen;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().ID;
                    return RedirectToAction("index", "TrangChu", new { area = "" });
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }



        //create a string MD5
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


        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string Email)
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

                return View("Login");
            }
            else
            {
                return View("Login");
            }

        }
    }

}

