using System;
using System.Collections.Generic;
using System.Text;
using Assignment2.BLL.Models;
using Assignment2.DAL;
namespace Assignment2.BLL.Mappers
{
    class UserMapper
    {
        public User map(UserModel user)
        {
            if (user == null)
                return null;
            var newUser = new User() { ID = user.ID, Email = user.Email, FullName = user.FullName, GroupCode = user.GroupCode, Hobby = user.Hobby, Password = user.Password, Token = user.Token, UserType = user.Type };
            return newUser;
        }
        public UserModel map(User user)
        {
            if (user == null)
                return null;
            var newUser = new UserModel() { ID = user.ID, Email = user.Email, FullName = user.FullName, GroupCode = user.GroupCode, Hobby = user.Hobby, Password = user.Password, Token = user.Token, Type = user.UserType };
            return newUser;
        }
    }
}
