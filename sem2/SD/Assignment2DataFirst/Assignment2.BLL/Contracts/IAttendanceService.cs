using System;
using System.Collections.Generic;
using System.Text;
using Assignment2.BLL.Models;
using Assignment2.DAL;

namespace Assignment2.BLL.Contracts
{
    public interface IAttendanceService
    {
        void Add(AttendanceModel attendanceModel);

        void DeleteAttendance(AttendanceModel attendanceModel);

        List<AttendanceModel> GetAll();

        AttendanceModel GetById(int id);

        void UpdateAttendance(AttendanceModel attendanceModel);
    }
}
