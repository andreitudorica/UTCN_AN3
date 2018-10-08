using Assignment3.Models;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Assignment3.Areas.Student.Controllers
{
    public class AssignmentController : Controller
    {
        string Baseurl = "http://localhost:7486/";//"http://andreitudorica.ro/";
        int AssignmentID = 0;

        public ActionResult CreateSubmission(int? id)
        {
            if (Session["UserID"] != null && Session["UserType"].ToString() == "student" && id!=null)
            {
                SubmissionModel sub = new SubmissionModel();
                sub.StudentID = Convert.ToInt32(Session["UserID"]);
                sub.AssignmentID = id.GetValueOrDefault();
                return View(sub);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public async Task<ActionResult> AssignmentsIndex(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["UserID"] != null && Session["UserType"].ToString() == "student")
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
        

        [HttpPost]
        public async Task<ActionResult> CreateSubmission(SubmissionModel model)
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
                HttpRequestMessage Req = new HttpRequestMessage(HttpMethod.Post, Baseurl + "api/submission/SubmitAssignment/" + model.StudentID+"/"+model.AssignmentID+"/"+model.GitLink+"/"+model.Note);
                //Req.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(model), Encoding.ASCII, "application/json");
                var response = await client.PostAsync(Req.RequestUri, Req.Content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("AssignmentsIndex");
            }
            //}
            return View(model);
        }


    }
}