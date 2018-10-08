using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3.Models
{
    public class SubmissionModel
    {
        public int ID { get; set; }
        public int AssignmentID { get; set; }
        public int StudentID { get; set; }
        public string GitLink { get; set; }
        public string Note { get; set; }
        public Nullable<decimal> Grade { get; set; }
    }
}