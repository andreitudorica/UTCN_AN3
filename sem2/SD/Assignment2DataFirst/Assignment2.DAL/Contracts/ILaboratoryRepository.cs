using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.DAL.Contracts
{
    public interface ILaboratoryRepository
    {
        List<Laboratory> GetAll();

        void Add(Laboratory laboratory);

        Laboratory Update(Laboratory laboratory);

        void Delete(int id);

        void Delete(Laboratory laboratory);

        Laboratory GetById(int id);
        
    }
}
