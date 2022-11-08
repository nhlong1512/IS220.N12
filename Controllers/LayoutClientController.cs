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
        public ActionResult GetData()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                return View();
        }
    }
}