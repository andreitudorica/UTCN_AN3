using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.ServicesLayer.Contracts
{
    interface IUserService
    {
        List<ServicesLayer.Models.UserModel> getAll();
        string login(string username, string password);
        bool nameExists(string name);
        bool IDExists(int ID);
        void add(string username, string password, string title);
        void update(int ID,string username, string password, string title);
        void delete(int ID);
    }
}
