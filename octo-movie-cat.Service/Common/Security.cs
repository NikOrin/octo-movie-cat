using octo_movie_cat.Contracts;
using octo_movie_cat.Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Service.Common
{
    public static class Security
    {
        public static string EncryptPassword(string password, byte[] salt)
        {
            var k1 = new Rfc2898DeriveBytes(password, salt, 1000);

            var hash = k1.GetBytes(32);

            var fullHash = new byte[48];

            Array.Copy(salt, 0, fullHash, 0, 16);
            Array.Copy(hash, 0, fullHash, 16, 32);

            var password_e = Convert.ToBase64String(fullHash);

            return password_e;
        }

        public static byte[] GenerateSalt()
        {
            var salt = new byte[16];
            var rngCryptographer = new RNGCryptoServiceProvider();
            rngCryptographer.GetBytes(salt = new byte[16]);

            return salt;
        }

        public static bool AuthenticateUser(int userID, string username, string password)
        {
            var userService = new UserService();
            var userAuthentication = userService.GetUserAuthenticationObject(userID);

            var testPassword = EncryptPassword(password, Convert.FromBase64String(userAuthentication.Salt));

            return userAuthentication.Password_e.Equals(testPassword) 
                && userAuthentication.Username.Equals(username);
        }
    }
}
