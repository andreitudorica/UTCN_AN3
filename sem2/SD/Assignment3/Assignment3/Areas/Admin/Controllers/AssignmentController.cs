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
    public class AssignmentController : Controller
    {
        string Baseurl = "http://localhost:7486/";//"http://andreitudorica.ro/";
        #region assignment

        public async Task<ActionResult> AssignmentsEdit(int? id)
        {
            if (Session["UserID"] != null && Session["UserType"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AssignmentModel assignment = null;
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        string auth = Session["UserEmail"].ToString() + ":" + Session["UserPassword"];
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", auth);
                        HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Get, Baseurl + "api/Assignment/" + id);
                        var response = await client.GetAsync(Req.RequestUri);
                        var jsonString = await response.Content.ReadAsStringAsync();
                        assignment = JsonConvert.DeserializeObject<AssignmentModel>(jsonString);
                    }
                }
                if (assignment == null)
                {
                    return HttpNotFound();
                }


                return View(assignment);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public async Task<ActionResult> AssignmentsDelete(int? id, bool? saveChangesError = false)
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
                AssignmentModel student = null;
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        string auth = Session["UserEmail"].ToString() + ":" + Session["UserPassword"];
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", auth);
                        HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Get, Baseurl + "api/Assignment/" + id);
                        var response = await client.GetAsync(Req.RequestUri);
                        var jsonString = await response.Content.ReadAsStringAsync();
                        student = JsonConvert.DeserializeObject<AssignmentModel>(jsonString);
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
        public async Task<ActionResult> AssignmentsIndex(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["UserID"] != null && Session["UserType"].ToString() == "admin")
            {
                List<AssignmentModel> students = new List<AssignmentModel>();
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
                        HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Get, Baseurl + "api/Assignment");
                        var response = await client.GetAsync(Req.RequestUri);
                        var jsonString = await response.Content.ReadAsStringAsync();
                        students = JsonConvert.DeserializeObject<List<AssignmentModel>>(jsonString);
                    }
                }

                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(students.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public async Task<ActionResult> SeeGrades(int id, string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["UserID"] != null && Session["UserType"].ToString() == "admin")
            {
                List<GradeModel> grades = new List<GradeModel>();
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
                        HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Get, Baseurl + "/api/submission/GetGradesForAssignment/" + id);
                        var response = await client.GetAsync(Req.RequestUri);
                        var jsonString = await response.Content.ReadAsStringAsync();
                        grades = JsonConvert.DeserializeObject<List<GradeModel>>(jsonString);
                    }
                }

                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(grades.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }


        [HttpPost]
        public async Task<ActionResult> AssignmentsCreate(AssignmentModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string auth = Session["UserEmail"].ToString() + ":" + Session["UserPassword"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", auth);
                HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Post, Baseurl + "api/Assignment");
                Req.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(model), Encoding.ASCII, "application/json");
                var response = await client.PostAsync(Req.RequestUri, Req.Content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("AssignmentsIndex");
                else
                    ModelState.AddModelError("", "Laboratory doesn't exist");
            }
            return View();
        }


        [HttpPost, ActionName("AssignmentsEdit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignmentEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignmentModel assignmentToUpdate = null;
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string auth = Session["UserEmail"].ToString() + ":" + Session["UserPassword"];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", auth);
                    HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Get, Baseurl + "api/Assignment/" + id);
                    var response = await client.GetAsync(Req.RequestUri);
                    var jsonString = await response.Content.ReadAsStringAsync();
                    assignmentToUpdate = JsonConvert.DeserializeObject<AssignmentModel>(jsonString);
                }
            }

            if (TryUpdateModel(assignmentToUpdate, "",
               new string[] { "LabID", "Title", "Deadline", "Description" }))
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
                            HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Put, Baseurl + "api/Assignment");
                            Req.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(assignmentToUpdate), Encoding.ASCII, "application/json");
                            var response = await client.PutAsync(Req.RequestUri, Req.Content);
                            if (response.IsSuccessStatusCode)
                                return RedirectToAction("AssignmentsIndex");
                        }
                    }
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(assignmentToUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignmentsDelete(int id)
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

                    HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Delete, Baseurl + "api/Assignment/" + id);
                    var response = await client.DeleteAsync(Req.RequestUri);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("AssignmentsIndex");

                }
            }

            return RedirectToAction("AssignmentsDelete", new { id = id, saveChangesError = true });
        }
        #endregion
    }
}