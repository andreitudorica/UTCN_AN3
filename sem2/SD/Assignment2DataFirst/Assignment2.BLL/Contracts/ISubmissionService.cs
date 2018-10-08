using System;
using System.Collections.Generic;
using System.Text;
using Assignment2.BLL.Models;
using Assignment2.DAL;

namespace Assignment2.BLL.Contracts
{
    public interface ISubmissionService
    {
        void Add(SubmissionModel submissionModel);

        void DeleteSubmission(SubmissionModel submissionModel);

        List<SubmissionModel> GetAll();

        SubmissionModel GetById(int id);

        void UpdateSubmission(SubmissionModel submissionModel);

        SubmissionModel UpdateGrade(int ID, decimal grade);
    }
}
