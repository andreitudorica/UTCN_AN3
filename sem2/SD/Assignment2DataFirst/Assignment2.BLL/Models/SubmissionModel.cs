using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2.BLL.Models
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
