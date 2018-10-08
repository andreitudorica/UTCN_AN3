using System;
using System.Collections.Generic;
using System.Text;
using Assignment2.BLL.Models;
using Assignment2.DAL;

namespace Assignment2.BLL.Mappers
{
    class AttendanceMapper
    {
        public Attendance map(AttendanceModel attendance)
        {
            if (attendance == null)
                return null;
            var newAttendance = new Attendance() { ID = attendance.ID, LaboratoryID = attendance.LaboratoryID, StudentID = attendance.StudentID };
            return newAttendance;
        }
        public AttendanceModel map(Attendance attendance)
        {
            if (attendance == null)
                return null;
            var newAttendance = new AttendanceModel() { ID = attendance.ID, LaboratoryID = attendance.LaboratoryID, StudentID = attendance.StudentID };
            return newAttendance;
        }
    }
}
