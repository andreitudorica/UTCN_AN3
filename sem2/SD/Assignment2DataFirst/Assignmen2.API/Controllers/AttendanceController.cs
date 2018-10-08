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
    public class AttendanceController : ApiController
    {
        private IAttendanceService attendanceService;
        public AttendanceController(IAttendanceService attendanceService)
        {
            this.attendanceService = attendanceService;
        }
        // GET: api/attendance
        [Authorize(Roles = "admin")]
        public IEnumerable<AttendanceModel> Get()
        {
            return attendanceService.GetAll();
        }

        // GET: api/attendance/5
        [Authorize(Roles = "admin")]
        public AttendanceModel Get(int id)
        {
            return attendanceService.GetById(id);
        }

        // POST: api/attendance
        [Authorize(Roles = "admin")]
        public void Post([FromBody]AttendanceModelAPI attendanceModelAPI)
        {
            var user = new AttendanceModel() { LaboratoryID = attendanceModelAPI.LaboratoryID, StudentID = attendanceModelAPI.StudentID };
            attendanceService.Add(user);
        }

        // PUT: api/attendance/5
        [Authorize(Roles = "admin")]
        public void Put([FromBody]AttendanceModel attendanceModel)
        {
            attendanceService.UpdateAttendance(attendanceModel);
        }

        // DELETE: api/attendance/5
        [Authorize(Roles = "admin")]
        public void Delete(int id)
        {
            attendanceService.DeleteAttendance(attendanceService.GetById(id));
        }
    }
}
