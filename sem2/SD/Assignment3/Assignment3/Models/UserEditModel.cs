﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3.Models
{
    public class UserEditModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string GroupCode { get; set; }
        public string FullName { get; set; }
        public string Hobby { get; set; }
    }
}