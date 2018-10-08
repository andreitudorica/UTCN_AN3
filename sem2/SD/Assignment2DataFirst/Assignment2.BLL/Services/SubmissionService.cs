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
    public class SubmissionService :ISubmissionService
    {
        private ISubmissionRepository submissionRepository;
        private SubmissionMapper mapper = new SubmissionMapper();
        public SubmissionService(ISubmissionRepository submissionRepository)
        {
            this.submissionRepository = submissionRepository;
        }

        //public SubmissionService()
        //{
        //    submissionRepository = new SubmissionRepository();
        //}

        public void Add(SubmissionModel submissionModel)
        {
            var newSubmission = mapper.map(submissionModel);
            submissionRepository.Add(newSubmission);
        }

        public void DeleteSubmission(SubmissionModel submissionModel)
        {
            submissionRepository.Delete(submissionModel.ID);
        }

        public List<SubmissionModel> GetAll()
        {
            var res = submissionRepository.GetAll();
            List<SubmissionModel> submissionModels = new List<SubmissionModel>();
            foreach (Submission s in res)
                submissionModels.Add(mapper.map(s));
            return submissionModels;
        }

        public SubmissionModel GetById(int id)
        {
            return mapper.map(submissionRepository.GetById(id));
        }

        public void UpdateSubmission(SubmissionModel submissionModel)
        {
            submissionRepository.Update(mapper.map(submissionModel));
        }
        
        public SubmissionModel UpdateGrade(int ID,decimal grade)
        {
            submissionRepository.UpdateGrade(ID, grade);
            return mapper.map(submissionRepository.GetById(ID));
        }
    }
}
