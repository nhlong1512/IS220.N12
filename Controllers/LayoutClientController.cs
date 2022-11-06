using Model.Dao;
using Model.EF;
using MoriiCoffee.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoriiCoffee.Controllers
{
    public class LayoutClientController : Controller
    {
        // GET: LayoutClient
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private NguoiDungDao nddao = new NguoiDungDao();
        private NguoiDung nd = new NguoiDung();
        public ActionResult GetData()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            ViewBag.session = session;
            if(session.UserID != null)
            {
                var nd = nddao.ViewDetail(session.UserID);
                ViewBag.nd = nd;
            }
            return View();
        }
    }
}