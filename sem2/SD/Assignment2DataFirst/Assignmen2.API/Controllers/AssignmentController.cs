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
    public class AssignmentController : ApiController
    {
        private IAssignmentService assignmentService;
        public AssignmentController(IAssignmentService assignmentService)
        {
            this.assignmentService = assignmentService;
        }
        // GET: api/Assignment
        [Authorize]
        public IEnumerable<AssignmentModel> Get()
        {
            return assignmentService.GetAll();
        }

        // GET: api/Assignment/5
        [Authorize]
        public AssignmentModel Get(int id)
        {
            return assignmentService.GetById(id);
        }

        [Authorize]
        [Route("api/Assignment/GetByLab/{LabID}")]
        public List<AssignmentModel> GetByLabID(int LabID)
        {
            return assignmentService.GetAll().Where(o => o.LabID == LabID).ToList();
        }

        // POST: api/Assignment
        [Authorize(Roles = "admin")]
        public void Post([FromBody]AssignmentModelAPI assignmentModelAPI)
        {
            var user = new AssignmentModel() { Deadline = assignmentModelAPI.Deadline, LabID = assignmentModelAPI.LabID, Title = assignmentModelAPI.Title, Description = assignmentModelAPI.Description };
            assignmentService.Add(user);
        }

        // PUT: api/Assignment/5
        [Authorize(Roles = "admin")]
        public void Put([FromBody]AssignmentModel assignmentModel)
        {
            assignmentService.UpdateAssignment(assignmentModel);
        }

        // DELETE: api/Assignment/5
        [Authorize(Roles = "admin")]
        public void Delete(int id)
        {
            assignmentService.DeleteAssignment(assignmentService.GetById(id));
        }
    }
}