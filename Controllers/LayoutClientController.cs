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
        public ActionResult GetData()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            ViewBag.session = session;
            return View();
        }
    }
}