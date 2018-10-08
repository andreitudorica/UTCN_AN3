using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.DAL.Contracts;

namespace Assignment2.DAL.Repositories
{
    public class LaboratoryRepository: ILaboratoryRepository
    {
        private Assignment2Entities db = new Assignment2Entities();

        public void Add(Laboratory laboratory)
        {
            db.Laboratories.Add(laboratory);
            db.SaveChanges();
        }

        public void Delete(int ID)
        {
            foreach (Assignment a in db.Assignments)
            {
                if (a.LabID == ID)
                    db.Assignments.Remove(a);
            }
            foreach (Attendance a in db.Attendances)
            {
                if (a.LaboratoryID == ID)
                {
                    foreach (Submission s in db.Submissions)
                    {
                        if (s.AssignmentID == ID)
                            db.Submissions.Remove(s);
                    }
                    db.Attendances.Remove(a);
                }
            }
            db.Laboratories.Remove(GetById(ID));
            db.SaveChanges();
        }

        public void Delete(Laboratory laboratory)
        {
            db.Laboratories.Remove(laboratory);
            db.SaveChanges();
        }

        public List<Laboratory> GetAll()
        {
            return db.Laboratories.ToList();
        }

        public Laboratory GetById(int id)
        {
            return db.Laboratories.FirstOrDefault(o => o.ID == id);
        }

        public Laboratory Update(Laboratory laboratory)
        {
            var lab = db.Laboratories.FirstOrDefault(o => o.ID == laboratory.ID);
            db.Entry(lab).CurrentValues.SetValues(laboratory);
            db.SaveChanges();
            return lab;
        }
    }
}
