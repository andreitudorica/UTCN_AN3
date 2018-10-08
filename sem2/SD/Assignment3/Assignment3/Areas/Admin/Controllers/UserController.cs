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

namespace Assignment3.Controllers
{
    public class UserController : Controller
    {
        string Baseurl = "http://localhost:7486/";//"http://andreitudorica.ro/";
        #region student
        public ActionResult StudentsIndex()
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

        public ActionResult StudentsCreate()
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

        public async Task<ActionResult> StudentsEdit(int? id)
        {
            if (Session["UserID"] != null && Session["UserType"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UserModel student = null;
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        string auth = Session["UserEmail"].ToString() + ":" + Session["UserPassword"];
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", auth);
                        HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Get, Baseurl + "api/User/" + id);
                        var response = await client.GetAsync(Req.RequestUri);
                        var jsonString = await response.Content.ReadAsStringAsync();
                        student = JsonConvert.DeserializeObject<UserModel>(jsonString);
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

        public async Task<ActionResult> StudentsDelete(int? id, bool? saveChangesError = false)
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
                UserModel student = null;
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        string auth = Session["UserEmail"].ToString() + ":" + Session["UserPassword"];
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", auth);
                        HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Get, Baseurl + "api/User/" + id);
                        var response = await client.GetAsync(Req.RequestUri);
                        var jsonString = await response.Content.ReadAsStringAsync();
                        student = JsonConvert.DeserializeObject<UserModel>(jsonString);
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
        public async Task<ViewResult> StudentsIndex(string sortOrder, string currentFilter, string searchString, int? page)
        {
            List<UserModel> students = new List<UserModel>();
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
                    HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Get, Baseurl + "api/User");
                    var response = await client.GetAsync(Req.RequestUri);
                    var jsonString = await response.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<UserModel>>(jsonString);
                    students = students.Where(o => o.Type == "student" /*&& o.FullName != null*/).ToList();
                }
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<ActionResult> StudentsCreate(UserCreateModel model)
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
                    HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Post, Baseurl + "api/User");
                    Req.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(model), Encoding.ASCII, "application/json");
                    var response = await client.PostAsync(Req.RequestUri, Req.Content);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("StudentsIndex");
                }
            }
            return View();
        }

        private UserEditModel convertToEditModel(UserModel usr)
        {
            UserEditModel res = new UserEditModel() { Email = usr.Email, FullName = usr.FullName, GroupCode = usr.GroupCode, Hobby = usr.Hobby, Password = usr.Password };
            return res;
        }

        [HttpPost, ActionName("StudentsEdit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> StudentEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel studentToUpdate = null;
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string auth = Session["UserEmail"].ToString() + ":" + Session["UserPassword"];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", auth);
                    HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Get, Baseurl + "api/User/" + id);
                    var response = await client.GetAsync(Req.RequestUri);
                    var jsonString = await response.Content.ReadAsStringAsync();
                    studentToUpdate = JsonConvert.DeserializeObject<UserModel>(jsonString);
                }
            }

            if (TryUpdateModel(studentToUpdate, "",
               new string[] { "Email", "FullName", "GroupCode", "Hobby" }))
            {
                try
                {
                    UserEditModel usrModel = convertToEditModel(studentToUpdate);
                    if (ModelState.IsValid)
                    {
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(Baseurl);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Put, Baseurl + "api/User?token=" + studentToUpdate.Token);
                            Req.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(usrModel), Encoding.ASCII, "application/json");
                            var response = await client.PutAsync(Req.RequestUri, Req.Content);
                            if (response.IsSuccessStatusCode)
                                return RedirectToAction("StudentsIndex");
                        }
                    }
                    return RedirectToAction("StudentsIndex");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> StudentsDelete(int id)
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

                    HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Delete, Baseurl + "api/User/" + id);
                    var response = await client.DeleteAsync(Req.RequestUri);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("StudentsIndex");

                }
            }

            return RedirectToAction("StudentsDelete", new { id = id, saveChangesError = true });
        }
        #endregion
    }
}