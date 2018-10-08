using System;
using System.Collections.Generic;
using System.Text;
using Assignment2.BLL.Contracts;
using Assignment2.BLL.Models;
using Assignment2.BLL.Mappers;
using Assignment2.DAL;
using Assignment2.DAL.Contracts;
using Assignment2.DAL.Repositories;

namespace Assignment2.BLL.Services
{
    public class AssignmentService : IAssignmentService
    {
        private IAssignmentRepository assignmentRepository;
        private AssignmentMapper mapper = new AssignmentMapper();
        public AssignmentService(IAssignmentRepository assignmentRepository)
        {
            this.assignmentRepository = assignmentRepository;
        }

        //public AssignmentService()
        //{
        //    assignmentRepository = new AssignmentRepository();
        //}

        public void Add(AssignmentModel assignmentModel)
        {
            var newAssignment = mapper.map(assignmentModel);
            assignmentRepository.Add(newAssignment);
        }

        public void DeleteAssignment(AssignmentModel assignmentModel)
        {
            assignmentRepository.Delete(assignmentModel.ID);
        }

        public List<AssignmentModel> GetAll()
        {
            var res = assignmentRepository.GetAll();
            List<AssignmentModel> assignmentModels = new List<AssignmentModel>();
            foreach (Assignment a in res)
                assignmentModels.Add(mapper.map(a));
            return assignmentModels;
        }

        public AssignmentModel GetById(int id)
        {
            return mapper.map(assignmentRepository.GetById(id));
        }

        public void UpdateAssignment(AssignmentModel assignmentModel)
        {
            assignmentRepository.Update(mapper.map(assignmentModel));
        }
    }
}
