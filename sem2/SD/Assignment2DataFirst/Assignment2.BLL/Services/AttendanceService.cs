using System;
using System.Collections.Generic;
using System.Text;
using Assignment2.BLL.Contracts;
using Assignment2.BLL.Models;
using Assignment2.BLL.Mappers;
using Assignment2.DAL;
using Assignment2.DAL.Contracts;
using Assignment2.DAL.Repositories;

namespace Assignment2.BLL.Services
{
    public class AttendanceService:IAttendanceService
    {
        private IAttendanceRepository attendanceRepository;
        private AttendanceMapper mapper = new AttendanceMapper();
        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            this.attendanceRepository = attendanceRepository;
        }

        //public AttendanceService()
        //{
        //    attendanceRepository = new AttendanceRepository();
        //}

        public void Add(AttendanceModel attendanceModel)
        {
            var newAttendance = mapper.map(attendanceModel);
            attendanceRepository.Add(newAttendance);
        }

        public void DeleteAttendance(AttendanceModel attendanceModel)
        {
            attendanceRepository.Delete(attendanceModel.ID);
        }

        public List<AttendanceModel> GetAll()
        {
            var res = attendanceRepository.GetAll();
            List<AttendanceModel> attendanceModels = new List<AttendanceModel>();
            foreach (Attendance a in res)
                attendanceModels.Add(mapper.map(a));
            return attendanceModels;
        }

        public AttendanceModel GetById(int id)
        {
            return mapper.map(attendanceRepository.GetById(id));
        }

        public void UpdateAttendance(AttendanceModel attendanceModel)
        {
            attendanceRepository.Update(mapper.map(attendanceModel));
        }
    }
}
