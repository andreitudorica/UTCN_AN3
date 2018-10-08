using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.DAL.Contracts
{
    public interface IAttendanceRepository
    {
        List<Attendance> GetAll();

        void Add(Attendance attendance);

        Attendance Update(Attendance attendance);

        void Delete(int id);

        void Delete(Attendance attendance);

        Attendance GetById(int id);
    }
}
