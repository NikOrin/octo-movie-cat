using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using octo_movie_cat.Contracts;

namespace octo_movie_cat.Service.User
{
    public class UserEntity
    {
        public int? UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
