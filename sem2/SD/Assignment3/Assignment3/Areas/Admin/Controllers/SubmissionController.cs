using Assignment3.Models;
using Newtonsoft.Json;
using PagedList;
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


namespace Assignment3.Areas.Admin.Controllers
{
    public class SubmissionController : Controller
    {
        string Baseurl = "http://localhost:7486/";//"http://andreitudorica.ro/";
        #region Submission
        public ActionResult SubmissionsIndex()
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
 
        public async Task<ActionResult> SubmissionsEdit(int? id)
        {
            if (Session["UserID"] != null && Session["UserType"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SubmissionModel Submission = null;
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        string auth = Session["UserEmail"].ToString() + ":" + Session["UserPassword"];
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", auth);
                        HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Get, Baseurl + "api/Submission/" + id);
                        var response = await client.GetAsync(Req.RequestUri);
                        var jsonString = await response.Content.ReadAsStringAsync();
                        Submission = JsonConvert.DeserializeObject<SubmissionModel>(jsonString);
                    }
                }
                if (Submission == null)
                {
                    return HttpNotFound();
                }


                return View(Submission);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public async Task<ViewResult> SubmissionsIndex(string sortOrder, string currentFilter, string searchString, int? page)
        {
            List<SubmissionModel> students = new List<SubmissionModel>();
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
                    HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Get, Baseurl + "api/Submission");
                    var response = await client.GetAsync(Req.RequestUri);
                    var jsonString = await response.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<SubmissionModel>>(jsonString);
                }
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost, ActionName("SubmissionsEdit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubmissionEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmissionModel SubmissionToUpdate = null;
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string auth = Session["UserEmail"].ToString() + ":" + Session["UserPassword"];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", auth);
                    HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Get, Baseurl + "api/Submission/" + id);
                    var response = await client.GetAsync(Req.RequestUri);
                    var jsonString = await response.Content.ReadAsStringAsync();
                    SubmissionToUpdate = JsonConvert.DeserializeObject<SubmissionModel>(jsonString);
                }
            }

            if (TryUpdateModel(SubmissionToUpdate, "",
               new string[] { "Grade" }))
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        string auth = Session["UserEmail"].ToString() + ":" + Session["UserPassword"];
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", auth);
                        HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Put, Baseurl + "api/Submission");
                        Req.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(SubmissionToUpdate), Encoding.ASCII, "application/json");
                        var response = await client.PutAsync(Req.RequestUri, Req.Content);
                        if (response.IsSuccessStatusCode)
                            return RedirectToAction("SubmissionsIndex");
                        else
                            ModelState.AddModelError("", "Unable to save changes. Try again");
                    }
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again");
                }
            }
            return View(SubmissionToUpdate);
        }

        #endregion
    }
}