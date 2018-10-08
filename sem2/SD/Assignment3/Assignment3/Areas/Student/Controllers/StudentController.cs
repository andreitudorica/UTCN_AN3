using Assignment3.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Assignment3.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            if (Session["UserID"] != null && Session["UserType"].ToString() == "student")
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