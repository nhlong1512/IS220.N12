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
            //if (ModelState.IsValid)
            //{
            //    var session = new UserLogin();
            //    session = (UserLogin)Session[CommonConstants.USER_SESSION];

            //    if (!(session is null))
            //    {
            //        ViewBag.session = session;
            //        var nd = nguoidungdao.ViewDetailEmail(session.UserName);
            //        if(nd.GioiTinh is null)
            //        {
            //            nd.GioiTinh = true;
            //        }
            //        ViewBag.ndd = nd;
            //    }

            //}
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
    }
}