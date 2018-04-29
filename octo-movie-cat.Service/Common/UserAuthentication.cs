using octo_movie_cat.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Service.Common
{
    internal class UserAuthentication
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password_e { get; set; }
        public string Salt { get; set; }
    }
}
