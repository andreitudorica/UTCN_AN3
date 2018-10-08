using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment2.BLL.Contracts;
using Assignment2.BLL.Services;
using Assignment2.BLL.Models;
using Assignment2.API.Models;
using System.Web.Http.Cors;

namespace Assignment2.API.Controllers
{
    public class CreateUser
    {
        public string Email { get; set; }
        public string Type { get; set; }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public UserController()
        {
            this.userService = new UserService();
        }
        // GET: api/User
        [Authorize(Roles = "admin")]
        public IEnumerable<UserModel> Get()
        {
            var users =  userService.GetAll();
            return users;
        }

        // GET: api/User/5
        [Authorize(Roles = "admin")]
        public UserModel Get(int id)
        {
            return userService.GetById(id);
        }

        // POST: api/User
        [Authorize(Roles = "admin")]
        public string Post(CreateUser cu)
        {
            var user = new UserModel() { Type = cu.Type, Email = cu.Email };
            return userService.Add(user);
        }


        // PUT: api/User/5
        public void Put(string token, [FromBody]UserModelAPI userModelAPI)
        {
            var user = userService.GetByToken(token);
            user.Email = userModelAPI.Email;
            user.FullName = userModelAPI.FullName;
            user.GroupCode = userModelAPI.GroupCode;
            user.Hobby = userModelAPI.Hobby;
            user.Password = userModelAPI.Password;
            userService.UpdateUser(user);
        }
        //Login
        [Route("api/User/Login")]
        public UserModel Put([FromBody]UserModelAPI userModelAPI)
        {
            var user = userService.Login(userModelAPI.Email, userModelAPI.Password);
            return user;
            //if (user == null)
            //    return "Login failed";
            //return "Login succesful as " + user.Type;
        }

        // DELETE: api/User/5
        [Authorize(Roles = "admin")]
        public void Delete(int id)
        {
            userService.DeleteUser(userService.GetById(id));
        }
    }
}
