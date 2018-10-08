using System;
using System.Collections.Generic;
using System.Text;
using Assignment2.BLL.Models;
using Assignment2.DAL; 

namespace Assignment2.BLL.Contracts
{
    public interface IAssignmentService
    {
        void Add(AssignmentModel assignmentModel);

        void DeleteAssignment(AssignmentModel assignmentModel);

        List<AssignmentModel> GetAll();

        AssignmentModel GetById(int id);

        void UpdateAssignment(AssignmentModel assignmentModel);
    }
}
