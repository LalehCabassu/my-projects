using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Messages
{
    [ClassSerializationCode(30)]
    public class Login : Request
    {
        private string username = null;
        private string password = null;

        public Login() { }

        public Login(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }


    }
}
