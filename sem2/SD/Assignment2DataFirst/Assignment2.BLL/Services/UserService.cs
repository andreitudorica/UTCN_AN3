using System;
using System.Collections.Generic;
using System.Text;
using Assignment2.BLL.Contracts;
using Assignment2.BLL.Models;
using Assignment2.BLL.Mappers;
using Assignment2.DAL;
using Assignment2.DAL.Contracts;
using Assignment2.DAL.Repositories;
using MlkPwgen;
using System.Net.Mail;

namespace Assignment2.BLL.Services
{
    public class UserService : IUserService

    { 
        private string pw = "assignment3";
        private IUserRepository userRepository;
        private UserMapper mapper = new UserMapper();
        public UserService(IUserRepository iUserRepository)
        {
            userRepository = iUserRepository;
        }
        public UserService()
        {
            userRepository = new UserRepository();
        }
        //public UserService()
        //{
        //    userRepository = new UserRepository();
        //}

        private void sendMail(string email, string token)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("andrei.tudorica.test@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Your token for the school app";
                mail.Body = "Access the following link: http://localhost:50820/Home/Register. Use this token to register:\n"+token+"\n\nThank you!";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("andrei.tudorica.test@gmail.com", pw);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
            }
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public string Add(UserModel userModel)
        {
            if (IsValidEmail(userModel.Email))
            {
                userModel.Token = PasswordGenerator.Generate(length: 128, allowed: Sets.Alphanumerics);//to be changed to 128

                sendMail(userModel.Email, userModel.Token);

                var newUser = mapper.map(userModel);
                userRepository.Add(newUser);
                return "User Created";
            }
            return "invalid Email address";
        }

        public void DeleteUser(UserModel userModel)
        {
            userRepository.Delete(userModel.ID);
        }

        public List<UserModel> GetAll()
        {
            var res = userRepository.GetAll();
            List<UserModel> userModels = new List<UserModel>();
            foreach (User u in res)
                userModels.Add(mapper.map(u));
            return userModels;
        }

        public UserModel GetById(int id)
        {
            return mapper.map(userRepository.GetById(id));
        }

        public UserModel GetByToken(string token)
        {
            var user = userRepository.GetByToken(token);
            var userModel = mapper.map(user);
            return userModel;
        }

        public UserModel Login(string email, string password)
        {
            var user = userRepository.Login(email, password);
            var userModel = mapper.map(user);
            return userModel;
        }

        public void UpdateUser(UserModel userModel)
        {
            userRepository.Update(mapper.map(userModel));
        }
    }
}
