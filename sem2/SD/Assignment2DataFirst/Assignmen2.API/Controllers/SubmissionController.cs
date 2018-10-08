using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment2.BLL.Contracts;
using Assignment2.BLL.Services;
using Assignment2.BLL.Models;
using Assignment2.API.Models;

namespace Assignment2.API.Controllers
{
    public class SubmissionController : ApiController
    {
        private ISubmissionService submissionService;//=new SubmissionService();
        private IUserService userService;
        private IAssignmentService assignmentService;
        public SubmissionController(ISubmissionService service, IUserService userService, IAssignmentService assignmentService)
        {
            this.submissionService = service;
            this.userService = userService;
            this.assignmentService = assignmentService;
        }

        // GET: api/submission
        [Authorize(Roles = "admin")]
        public IEnumerable<SubmissionModel> Get()
        {
            return submissionService.GetAll();
        }

        // GET: api/submission/5
        [Authorize]
        public SubmissionModel Get(int id)
        {
            return submissionService.GetById(id);
        }

        [Route("api/submission/GetGradesForAssignment/{assignmentID}")]
        [Authorize(Roles = "admin")]
        public List<Tuple<string, decimal>> getGrades(int assignmentID)
        {
            List<Tuple<string, decimal>> result = new List<Tuple<string, decimal>>();
            foreach (SubmissionModel s in submissionService.GetAll().Where(o => o.AssignmentID == assignmentID))
            {
                if ((decimal?)s.Grade != null)
                {
                    var user = userService.GetById(s.StudentID);
                    result.Add(new Tuple<string, decimal>(user.FullName, (decimal)s.Grade));
                }
            }
            return result;
        }

        [Route("api/submission/SubmitAssignment/{StudentID}/{AssignmentID}/{GitLink}/{Note}")]
        [Authorize(Roles = "student")]
        public string Post(int StudentID, int AssignmentID, string GitLink, string Note)
        {

            var previousSubmission = submissionService.GetAll().FirstOrDefault(o => o.StudentID == StudentID && o.AssignmentID == AssignmentID);
            if (previousSubmission == null)
            {
                var submission = new SubmissionModel() { AssignmentID = AssignmentID, GitLink = GitLink, Grade = 0, Note = Note, StudentID = StudentID };
                submissionService.Add(submission);
                return "submission added succesfully";
            }
            return "This student already created a submission for this assignmnet";
        }

        [Route("api/submission/GradeSubmission/{id}/{grade}")]
        [Authorize(Roles = "admin")]
        public void GradeSubmission(int id, decimal grade)
        {
            submissionService.UpdateGrade(id, grade);
        }

        // PUT: api/submission/5
        [Authorize]
        public void Put([FromBody]SubmissionModel submissionModel)
        {
            submissionService.UpdateSubmission(submissionModel);
        }


        // DELETE: api/submission/5
        [Authorize(Roles = "admin")]
        public void Delete(int id)
        {
            submissionService.DeleteSubmission(submissionService.GetById(id));
        }
    }
}
