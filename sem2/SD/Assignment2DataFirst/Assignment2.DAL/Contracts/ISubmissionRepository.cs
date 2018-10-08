using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.DAL.Contracts
{
    public interface ISubmissionRepository
    {
        List<Submission> GetAll();

        void Add(Submission submission);

        Submission Update(Submission submission);

        void Delete(int id);

        void Delete(Submission submission);

        Submission GetById(int id);

        Submission UpdateGrade(int ID, decimal grade);
    }
}
