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
    public class AttendanceController : Controller
    {
        string Baseurl = "http://localhost:7486/";//"http://andreitudorica.ro/";
        #region attendance
        public ActionResult AttendancesIndex()
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

        public ActionResult AttendancesCreate()
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

        public async Task<ActionResult> AttendancesEdit(int? id)
        {
            if (Session["UserID"] != null && Session["UserType"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AttendanceModel Attendance = null;
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        string auth = Session["UserEmail"].ToString() + ":" + Session["UserPassword"];
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", auth);
                        HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Get, Baseurl + "api/Attendance/" + id);
                        var response = await client.GetAsync(Req.RequestUri);
                        var jsonString = await response.Content.ReadAsStringAsync();
                        Attendance = JsonConvert.DeserializeObject<AttendanceModel>(jsonString);
                    }
                }
                if (Attendance == null)
                {
                    return HttpNotFound();
                }


                return View(Attendance);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public async Task<ActionResult> AttendancesDelete(int? id, bool? saveChangesError = false)
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
                AttendanceModel student = null;
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        string auth = Session["UserEmail"].ToString() + ":" + Session["UserPassword"];
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", auth);
                        HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Get, Baseurl + "api/Attendance/" + id);
                        var response = await client.GetAsync(Req.RequestUri);
                        var jsonString = await response.Content.ReadAsStringAsync();
                        student = JsonConvert.DeserializeObject<AttendanceModel>(jsonString);
                    }
                }
                if (student == null)
                {
                    return HttpNotFound();
                }

                return View(student);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public async Task<ViewResult> AttendancesIndex(string sortOrder, string currentFilter, string searchString, int? page)
        {
            List<AttendanceModel> students = new List<AttendanceModel>();
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
                    HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Get, Baseurl + "api/Attendance");
                    var response = await client.GetAsync(Req.RequestUri);
                    var jsonString = await response.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<AttendanceModel>>(jsonString);
                }
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<ActionResult> AttendancesCreate(AttendanceModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string auth = Session["UserEmail"].ToString() + ":" + Session["UserPassword"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", auth);
                HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Post, Baseurl + "api/Attendance");
                Req.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(model), Encoding.ASCII, "application/json");
                var response = await client.PostAsync(Req.RequestUri, Req.Content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("AttendancesIndex");
                else
                    ModelState.AddModelError("", "Laboratory doesn't exist");
            }
            return View();
        }


        [HttpPost, ActionName("AttendancesEdit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AttendanceEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttendanceModel AttendanceToUpdate = null;
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string auth = Session["UserEmail"].ToString() + ":" + Session["UserPassword"];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", auth);
                    HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Get, Baseurl + "api/Attendance/" + id);
                    var response = await client.GetAsync(Req.RequestUri);
                    var jsonString = await response.Content.ReadAsStringAsync();
                    AttendanceToUpdate = JsonConvert.DeserializeObject<AttendanceModel>(jsonString);
                }
            }

            if (TryUpdateModel(AttendanceToUpdate, "",
               new string[] { "LaboratoryID", "StudentID" }))
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
                            HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Put, Baseurl + "api/Attendance");
                            Req.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(AttendanceToUpdate), Encoding.ASCII, "application/json");
                            var response = await client.PutAsync(Req.RequestUri, Req.Content);
                            if (response.IsSuccessStatusCode)
                                return RedirectToAction("AttendancesIndex");
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
            return View(AttendanceToUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AttendancesDelete(int id)
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

                    HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Delete, Baseurl + "api/Attendance/" + id);
                    var response = await client.DeleteAsync(Req.RequestUri);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("AttendancesIndex");

                }
            }

            return RedirectToAction("AttendancesDelete", new { id = id, saveChangesError = true });
        }
        #endregion
    }
}