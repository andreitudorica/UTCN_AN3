using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.DAL.Contracts;

namespace Assignment2.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private Assignment2Entities db = new Assignment2Entities();

        public void Add(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void Delete(int ID)
        {
            db.Users.Remove(GetById(ID));
            db.SaveChanges();
        }

        public void Delete(User user)
        {
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public List<User> GetAll()
        {
            return db.Users.ToList();
        }

        public User GetById(int id)
        {
           return  db.Users.FirstOrDefault(o => o.ID == id);
        }

        public User GetByToken(string token)
        {
            return db.Users.FirstOrDefault(o => o.Token == token);
        }

        public User Update(User user)
        {
            var usr = db.Users.FirstOrDefault(o => o.ID == user.ID);
      
            db.Entry(usr).CurrentValues.SetValues(user);
            db.SaveChanges();
            return usr; 
        }

        public User Login(string email,string password)
        {
            return db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
