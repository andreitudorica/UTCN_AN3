using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.API.Models
{
    public class AssignmentModelAPI
    {
        public int ID { get; set; }
        public int LabID { get; set; }
        public string Title { get; set; }
        public System.DateTime Deadline { get; set; }
        public string Description { get; set; }
    }
}