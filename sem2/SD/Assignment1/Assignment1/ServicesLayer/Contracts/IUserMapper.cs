using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.ServicesLayer.Contracts
{
    interface IUserMapper
    {
        ServicesLayer.Models.UserModel map(DataLayer.Models.UserDTO dto);
        DataLayer.Models.UserDTO map(ServicesLayer.Models.UserModel model);
    }
}
