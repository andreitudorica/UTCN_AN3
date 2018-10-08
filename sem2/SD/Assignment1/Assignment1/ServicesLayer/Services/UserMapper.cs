using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.ServicesLayer.Services
{
    class UserMapper: ServicesLayer.Contracts.IUserMapper
    {
        public ServicesLayer.Models.UserModel map(DataLayer.Models.UserDTO dto)
        {
            ServicesLayer.Models.UserModel model = new ServicesLayer.Models.UserModel();
            model.Username = dto.Username;
            model.Title = dto.Title;
            model.Password = dto.Password;
            model.ID = dto.ID;
            return model;
        }
        public DataLayer.Models.UserDTO map(ServicesLayer.Models.UserModel model)
        {
            DataLayer.Models.UserDTO dto = new DataLayer.Models.UserDTO();
            dto.ID = model.ID;
            dto.Username = model.Username;
            dto.Title = model.Title;
            dto.Password = model.Password;
            return dto;
        }
    }
}
