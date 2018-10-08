using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.DAL.Contracts;

namespace Assignment2.DAL.Repositories
{
    public class AttendanceRepository: IAttendanceRepository
    {
        private Assignment2Entities db = new Assignment2Entities();

        public void Add(Attendance attendance)
        {
            db.Attendances.Add(attendance);
            db.SaveChanges();
        }

        public void Delete(int ID)
        {
            db.Attendances.Remove(GetById(ID));
            db.SaveChanges();
        }

        public void Delete(Attendance attendance)
        {
            db.Attendances.Remove(attendance);
            db.SaveChanges();
        }

        public List<Attendance> GetAll()
        {
            return db.Attendances.ToList();
        }

        public Attendance GetById(int id)
        {
            return db.Attendances.FirstOrDefault(o => o.ID == id);
        }

        public Attendance Update(Attendance attendance)
        {
            var att = db.Attendances.FirstOrDefault(o => o.ID == attendance.ID);

            db.Entry(att).CurrentValues.SetValues(attendance);
            db.SaveChanges();
            return att;
        }
    }
}
