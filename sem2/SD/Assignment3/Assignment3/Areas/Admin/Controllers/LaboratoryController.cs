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
namespace Assignment3.Controllers.Admin
{
    public class LaboratoryController : Controller
    {
        string Baseurl = "http://localhost:7486/";//"http://andreitudorica.ro/";
        #region Laboratory
        public ActionResult LaboratoriesIndex()
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

        public ActionResult LaboratoriesCreate()
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

        public async Task<ActionResult> LaboratoriesEdit(int? id)
        {
            if (Session["UserID"] != null && Session["UserType"].ToString() == "admin")
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

        public async Task<ActionResult> LaboratoriesDelete(int? id, bool? saveChangesError = false)
        {
            if (Session["UserID"] != null && Session["UserType"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (saveChangesError.GetValueOrDefault())
                {
                    ViewBag.ErrorMessage = "Delete failed. Try again!";
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

        [HttpPost]
        public async Task<ActionResult> LaboratoriesCreate(LaboratoryModel model)
        {
            //if (ModelState.IsValid)
            //{
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string auth = Session["UserEmail"].ToString() + ":" + Session["UserPassword"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", auth);
                HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Post, Baseurl + "api/Laboratory");
                Req.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(model), Encoding.ASCII, "application/json");
                var response = await client.PostAsync(Req.RequestUri, Req.Content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("LaboratoriesIndex");
            }
            //}
            return View();
        }

        [HttpPost, ActionName("LaboratoriesEdit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LabEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaboratoryModel LabToUpdate = null;
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
                    LabToUpdate = JsonConvert.DeserializeObject<LaboratoryModel>(jsonString);
                }
            }

            if (TryUpdateModel(LabToUpdate, "",
               new string[] { "Title", "Curricula", "LabDate", "Number", "Description" }))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(Baseurl);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            string auth = Session["UserEmail"].ToString() + ":" + Session["UserPassword"];
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", auth);
                            HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Put, Baseurl + "api/Laboratory");
                            Req.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(LabToUpdate), Encoding.ASCII, "application/json");
                            var response = await client.PutAsync(Req.RequestUri, Req.Content);
                            if (response.IsSuccessStatusCode)
                                return RedirectToAction("LaboratoriesIndex");
                        }
                    }
                    return RedirectToAction("LaboratoriesIndex");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(LabToUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LaboratoriesDelete(int id)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string auth = Session["UserEmail"].ToString() + ":" + Session["UserPassword"];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", auth);

                    HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Delete, Baseurl + "api/Laboratory/" + id);
                    var response = await client.DeleteAsync(Req.RequestUri);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("LaboratoriesIndex");

                }
            }

            return RedirectToAction("LaboratoriesDelete", new { id = id, saveChangesError = true });
        }
        #endregion
    }
}