using octo_movie_cat.Contracts;
using octo_movie_cat.Service.Common;
using octo_movie_cat.Service.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Service.Users
{
    public class UserService : BaseService
    {
        private static UserService _instance;

        public static UserService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserService();
                return _instance;
            }
        }

        private UserService() { }

        public int? CreateNewUser(UserContract user)
        {
            if (UsernameContainsUnpermittedCharacters(user.Username))
                return null;

            byte[] salt = Security.GenerateSalt();
            var hashedPassword = Security.EncryptPassword(user.Password, salt);

            user.Password = hashedPassword;
            user.Salt = Convert.ToBase64String(salt);

            var userEntity = new UserEntity();
            userEntity.UserID = user.UserID;
            userEntity.Username = user.Username;
            userEntity.Email = user.Email;
            userEntity.FirstName = user.FirstName;
            userEntity.LastName = user.LastName;
            userEntity.DateOfBirth = user.DateOfBirth;
            userEntity.CreatedOn = user.CreatedOn;
            userEntity.Password = user.Password;
            userEntity.Salt = user.Salt;

            int? userID = null;
            try
            {
                userID = UserRepository.Instance.CreateNewUser(userEntity);
            }
            catch (SqlException ex)
            {
                //log the error using a error logging service (splunk, etc.)
            }
            
            return userID;
        }

        public UserDetailsContract GetUserDetails(int userID)
        {
            var userDetails = new UserDetailsContract();
            return userDetails;
        }

        public UserContract GetUser(int userID)
        {
            UserEntity userEntity = UserRepository.Instance.GetUser(userID);

            var user = new UserContract();
            user.UserID = userEntity.UserID;
            user.Email = userEntity.Email;
            user.Username = userEntity.Username;
            user.FirstName = userEntity.FirstName;
            user.LastName = userEntity.LastName;
            user.DateOfBirth = userEntity.DateOfBirth;
            user.CreatedOn = userEntity.CreatedOn;
            user.UpdatedOn = userEntity.UpdatedOn;

            return user;
        }

        private bool UsernameContainsUnpermittedCharacters(string username)
        {
            return username.Contains(":");
        }
    }
}
