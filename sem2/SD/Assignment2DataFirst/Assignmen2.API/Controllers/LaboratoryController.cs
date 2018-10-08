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
    public class LaboratoryController : ApiController
    {
        private ILaboratoryService laboratoryService;
        public LaboratoryController(ILaboratoryService laboratoryService)
        {
            this.laboratoryService = laboratoryService;
        }
        // GET: api/Laboratory
        [Authorize]
        public IEnumerable<LaboratoryModel> Get()
        {
            return laboratoryService.GetAll();
        }

        // GET: api/Laboratory/Filter/keyword
        [Route("api/Laboratory/Filter/{keyword}")]
        [Authorize]
        public IEnumerable<LaboratoryModel> Get(string keyword)
        {
            return laboratoryService.GetAll().Where(o => o.Description.Contains(keyword) || o.Curricula.Contains(keyword));
        }

        // GET: api/Laboratory/5
        [Authorize]
        public LaboratoryModel Get(int id)
        {
            LaboratoryModel lab = laboratoryService.GetById(id);
            return lab;
        }

        // POST: api/Laboratory
        [Authorize(Roles = "admin")]
        public void Post([FromBody]LaboratoryModelAPI laboratoryModelAPI)
        {
            var user = new LaboratoryModel() { Curricula = laboratoryModelAPI.Curricula, Description = laboratoryModelAPI.Description, LabDate = laboratoryModelAPI.LabDate, Number = laboratoryModelAPI.Number, Title = laboratoryModelAPI.Title };
            laboratoryService.Add(user);
        }

        // PUT: api/Laboratory/5
        [Authorize(Roles = "admin")]
        public void Put([FromBody]LaboratoryModel laboratoryModel)
        {
            laboratoryService.UpdateLaboratory(laboratoryModel);
        }

        // DELETE: api/Laboratory/5
        [Authorize(Roles = "admin")]
        public void Delete(int id)
        {
            laboratoryService.DeleteLaboratory(laboratoryService.GetById(id));
        }
    }
}
