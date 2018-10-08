using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.DAL.Contracts;

namespace Assignment2.DAL.Repositories
{
    public class AssignmentRepository: IAssignmentRepository
    {
        private Assignment2Entities db = new Assignment2Entities();

        public void Add(Assignment assignment)
        {
            db.Assignments.Add(assignment);
            db.SaveChanges();
        }

        public void Delete(int ID)
        {
            foreach (Submission a in db.Submissions)
            {
                if (a.AssignmentID == ID)
                    db.Submissions.Remove(a);
            }
            db.Assignments.Remove(GetById(ID));
            db.SaveChanges();
        }

        public void Delete(Assignment assignment)
        {
            db.Assignments.Remove(assignment);
            db.SaveChanges();
        }

        public List<Assignment> GetAll()
        {
            return db.Assignments.ToList();
        }

        public Assignment GetById(int id)
        {
            return db.Assignments.FirstOrDefault(o => o.ID == id);
        }

        public Assignment Update(Assignment assignment)
        {
            var ass = db.Assignments.FirstOrDefault(o => o.ID == assignment.ID);
            db.Entry(ass).CurrentValues.SetValues(assignment);
            db.SaveChanges();
            return ass;
        }
    }
}
