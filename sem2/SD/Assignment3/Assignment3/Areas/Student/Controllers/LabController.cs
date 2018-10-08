using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Assignment3.Models;
using Newtonsoft.Json;
using PagedList;
using PagedList.Mvc;
namespace Assignment3.Controllers.Student
{
    public class LabController : Controller
    {
        string Baseurl = "http://localhost:7486/";//"http://andreitudorica.ro/";
        #region Laboratory
        public ActionResult LaboratoriesIndex()
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

     

      

        public async Task<ActionResult> LaboratoriesDetails(int? id)
        {
            if (Session["UserID"] != null && Session["UserType"].ToString() == "student")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LaboratoryModel laboratory = null;
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        string auth = Session["UserEmail"].ToString() + ":" + Session["UserPassword"];
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", auth);
                        HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Get, Baseurl + "api/Laboratory/" + id);
                        var response = await client.GetAsync(Req.RequestUri);
                        var jsonString = await response.Content.ReadAsStringAsync();
                        laboratory = JsonConvert.DeserializeObject<LaboratoryModel>(jsonString);
                    }
                }
                if (laboratory == null)
                {
                    return HttpNotFound();
                }

                return View(laboratory);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public async Task<ViewResult> LaboratoriesIndex(string sortOrder, string currentFilter, string searchString, int? page)
        {
            List<LaboratoryModel> laboratories = new List<LaboratoryModel>();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string auth = Session["UserEmail"].ToString() + ":" + Session["UserPassword"];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", auth);
                    HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Get, Baseurl + "api/Laboratory");
                    var response = await client.GetAsync(Req.RequestUri);
                    var jsonString = await response.Content.ReadAsStringAsync();
                    laboratories = JsonConvert.DeserializeObject<List<LaboratoryModel>>(jsonString);
                    //students = students.Where(o => o.Type == "student" /*&& o.FullName != null*/).ToList();
                }
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(laboratories.ToPagedList(pageNumber, pageSize));
        }
        
        #endregion
    }
}