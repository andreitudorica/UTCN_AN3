using Assignment3.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Assignment3.Controllers
{

    public class HomeController : Controller
    {
        string Baseurl = "http://localhost:7486/";//"http://andreitudorica.ro/";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            Session["UserID"] = null;
            Session["UserEmail"] = null;
            Session["UserType"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult PopMessage()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserModel objUser)
        {
            if (ModelState.IsValid)
            {
                //objUser.Type = "student";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Put, Baseurl+"api/User?token=" +objUser.Token);
                    Req.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(objUser),Encoding.ASCII, "application/json");
                    var response = await client.PutAsync(Req.RequestUri, Req.Content);
                    if (!response.IsSuccessStatusCode)
                        return PopMessage();
                    return RedirectToAction("Login");
                }
            }
            return View(objUser);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserEditModel objUser)
        {
            if (ModelState.IsValid)
            {
                UserModel obj = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Put, Baseurl + "api/User/Login");
                    Req.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(objUser), Encoding.ASCII, "application/json");
                    var Res = await client.PutAsync(Req.RequestUri, Req.Content);
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        obj = JsonConvert.DeserializeObject<UserModel>(EmpResponse);
                    }
                    if (obj != null)
                    {
                        Session["UserID"] = obj.ID.ToString();
                        Session["UserEmail"] = obj.Email.ToString();
                        Session["UserPassword"] = obj.Password.ToString();
                        Session["UserType"] = obj.Type;
                        if (obj.Type == "admin")
                            return RedirectToAction("Index", "Admin", new { area = "Admin" });
                        return RedirectToAction("Index", "Student", new { area = "Student" });
                    }
                }
            }
            return View(objUser);
        }

    }
}