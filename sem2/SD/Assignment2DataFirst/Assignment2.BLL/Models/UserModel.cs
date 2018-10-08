using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2.BLL.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string GroupCode { get; set; }
        public string FullName { get; set; }
        public string Hobby { get; set; }
    }
}
