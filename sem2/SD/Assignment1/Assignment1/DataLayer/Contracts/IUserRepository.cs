using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.DataLayer.Contracts
{
    interface IUserRepository
    {
        List<DataLayer.Models.UserDTO> getAll();
        bool create(DataLayer.Models.UserDTO user);
        bool update(DataLayer.Models.UserDTO user);
        bool delete(int ID);
    }
}
