//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assignment2.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attendance
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public int LaboratoryID { get; set; }
    
        public virtual Laboratory Laboratory { get; set; }
        public virtual User User { get; set; }
    }
}