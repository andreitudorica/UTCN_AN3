using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.API.Models
{
    public class AttendanceModelAPI
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public int LaboratoryID { get; set; }
    }
}