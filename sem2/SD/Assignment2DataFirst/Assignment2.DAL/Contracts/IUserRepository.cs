using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.DAL.Contracts
{
    public interface IUserRepository
    {
        List<User> GetAll();

        void Add(User user);

        User Update(User user);

        void Delete(int id);

        void Delete(User user);

        User GetById(int id);

        User GetByToken(string token);

        User Login(string email, string password);
    }
}
