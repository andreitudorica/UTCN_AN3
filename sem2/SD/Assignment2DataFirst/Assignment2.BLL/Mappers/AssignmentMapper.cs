using System;
using System.Collections.Generic;
using System.Text;
using Assignment2.BLL.Models;
using Assignment2.DAL;

namespace Assignment2.BLL.Mappers
{
    class AssignmentMapper
    {
        public Assignment map(AssignmentModel assignment)
        {
            if (assignment == null)
                return null;
            var newAssignment = new Assignment() { ID = assignment.ID, Deadline = assignment.Deadline, Description = assignment.Description, LabID = assignment.LabID, Title = assignment.Title };
            return newAssignment;
        }
        public AssignmentModel map(Assignment assignment)
        {
            if (assignment == null)
                return null;
            var newAssignment = new AssignmentModel() { ID = assignment.ID, Deadline = assignment.Deadline, Description = assignment.Description, LabID = assignment.LabID, Title = assignment.Title };
            return newAssignment;
        }
    }
}
