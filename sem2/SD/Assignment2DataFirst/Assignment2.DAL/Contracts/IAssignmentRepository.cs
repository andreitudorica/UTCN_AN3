using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.DAL.Contracts
{
    public interface IAssignmentRepository
    {
        List<Assignment> GetAll();

        void Add(Assignment assignment);

        Assignment Update(Assignment assignment);

        void Delete(int id);

        void Delete(Assignment assignment);

        Assignment GetById(int id);
    }
}
