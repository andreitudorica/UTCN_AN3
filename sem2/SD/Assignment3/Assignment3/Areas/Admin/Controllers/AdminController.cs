using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            if (Session["UserID"] != null && Session["UserType"].ToString() == "admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
    }
}