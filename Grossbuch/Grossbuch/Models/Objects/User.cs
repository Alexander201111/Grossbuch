using System;
using System.Collections.Generic;
using System.Text;

namespace Grossbuch.Models
{
    public class User
    {
        public int Id { get; set; }
        public int Id2 { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }

        //public List<UserAccount> UserAccounts { get; set; }

        public User()
        {
            Id = -1;
            Name = "";
            Password = "";
            //UserAccounts = new List<UserAccount>();
        }

        public User(string _login, string _password)
        {
            Login = _login;
            Password = _password;
            //UserAccounts = new List<UserAccount>();
        }

        public User(string _login, string _password, string _token)
        {
            Login = _login;
            Password = _password;
            Token = _token;
            //UserAccounts = new List<UserAccount>();
        }

        //public User(string _title, string _password, List<UserAccount> _userAccount)
        //{
        //    Name = _title;
        //    Password = _password;
        //    UserAccounts = _userAccount ?? new List<UserAccount>();
        //}
    }
}