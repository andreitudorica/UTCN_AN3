using System;
using System.Collections.Generic;
using System.Text;
using Assignment2.BLL.Models;
using Assignment2.DAL;

namespace Assignment2.BLL.Contracts
{
    public interface IUserService
    {
        string Add(UserModel userModel);

        void DeleteUser(UserModel userModel);

        List<UserModel> GetAll();

        UserModel GetById(int id);

        UserModel GetByToken(string token);

        void UpdateUser(UserModel userModel);

        UserModel Login(string email, string password);
    }
}
