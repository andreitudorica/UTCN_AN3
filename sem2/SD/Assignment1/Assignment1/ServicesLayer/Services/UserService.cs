using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.ServicesLayer.Services
{
    class UserService : ServicesLayer.Contracts.IUserService
    {
        private readonly DataLayer.Contracts.IUserRepository repository;
        private readonly ServicesLayer.Contracts.IUserMapper mapper;
        public UserService()
        {
            repository = new DataLayer.Repositories.UserRepository();
            mapper = new ServicesLayer.Services.UserMapper();
        }

        public List<ServicesLayer.Models.UserModel> getAll()
        {
            List<DataLayer.Models.UserDTO> userDTOs = repository.getAll();
            List<ServicesLayer.Models.UserModel> models = new List<Models.UserModel>();
            userDTOs.ForEach(x => models.Add(mapper.map(x)));
            return models;
        }

        public string login(string username, string password)
        {
            ServicesLayer.Models.UserModel model = this.getAll().FirstOrDefault(x => x.Username == username);
            if (model == null)
                return "failed";// "no user wes found with username: " + username+". Please try again!";
            if (model.Password == password)
                return model.Title;// "Logged in succesfully as " + username+"("+model.Title+")";
            return "failed";
        }

        public bool nameExists(string name)
        {
            if (getAll().Find(x => x.Username == name) == null)
                return false;
            return true;
        }

        public bool IDExists(int ID)
        {
            if (getAll().Find(x => x.ID == ID) == null)
                return false;
            return true;
        }

        public void add(string username, string password, string title)
        {
            ServicesLayer.Models.UserModel model = new Models.UserModel() { Username = username, Title = title, Password = password, ID = 0 };
            model.Password = model.encryptPassword(password);
            repository.create(mapper.map(model));
        }

        public void update(int ID, string username, string password, string title)
        {
            ServicesLayer.Models.UserModel model = new Models.UserModel() { Username = username, Title = title, Password = password, ID = ID };
            model.Password = model.encryptPassword(password);
            bool res;//for testing
            res = repository.update(mapper.map(model));
        }

        public void delete(int ID)
        {
            bool res;//for testing
            res = repository.delete(ID);
        }
    }
}
