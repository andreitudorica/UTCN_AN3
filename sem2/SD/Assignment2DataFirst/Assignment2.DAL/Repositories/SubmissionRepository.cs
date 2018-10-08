using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.DAL.Contracts;

namespace Assignment2.DAL.Repositories
{
    public class SubmissionRepository: ISubmissionRepository
    {
        private Assignment2Entities db = new Assignment2Entities();

        public void Add(Submission submission)
        {
            db.Submissions.Add(submission);
            db.SaveChanges();
        }

        public void Delete(int ID)
        {
            db.Submissions.Remove(GetById(ID));
            db.SaveChanges();
        }

        public void Delete(Submission submission)
        {
            db.Submissions.Remove(submission);
            db.SaveChanges();
        }

        public List<Submission> GetAll()
        {
            return db.Submissions.ToList();
        }

        public Submission GetById(int id)
        {
            return db.Submissions.FirstOrDefault(o => o.ID == id);
        }

        public Submission Update(Submission submission)
        {
            var sub = db.Submissions.FirstOrDefault(o => o.ID == submission.ID);
            if (sub != null)
            {
                db.Entry(sub).CurrentValues.SetValues(submission);
                db.SaveChanges();
            }
            return sub;
        }
        public Submission UpdateGrade(int ID, decimal grade)
        {
            var sub = db.Submissions.FirstOrDefault(o => o.ID == ID);
            if (sub != null)
            {
                sub.Grade = grade;
                db.SaveChanges();
            }
            return sub;
        }
    }
}
