using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterClone.Models
{
    public class UserInfo
    {
        public int UserInfoId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string UserDesc { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
    }
}