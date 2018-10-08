using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.DataLayer.Contracts
{
    interface IShowRepository
    {
        List<DataLayer.Models.ShowDTO> getAll();
        bool create(DataLayer.Models.ShowDTO user);
        bool update(DataLayer.Models.ShowDTO user);
        bool delete(int ID);
    }
}
