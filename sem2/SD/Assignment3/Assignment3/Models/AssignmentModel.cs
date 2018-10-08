using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3.Models
{
    public class AssignmentModel
    {
        public int ID { get; set; }
        public int LabID { get; set; }
        public string Title { get; set; }
        public System.DateTime Deadline { get; set; }
        public string Description { get; set; }
    }
}