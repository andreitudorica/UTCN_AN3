using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2.BLL.Models
{
    public class LaboratoryModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Curricula { get; set; }
        public System.DateTime LabDate { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
    }
}
