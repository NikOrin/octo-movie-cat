using octo_movie_cat.Contracts;
using octo_movie_cat.Service.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Service.Users
{
    public class UserService
    {
        public int? CreateNewUser(User user)
        {
            if (UsernameContainsUnpermittedCharacters(user.Username))
                return null;

            byte[] salt = Security.GenerateSalt();
            var hashedPassword = Security.EncryptPassword(user.Password, salt);

            user.Password = hashedPassword;
            user.Salt = Convert.ToBase64String(salt);

            using (var conn = new SqlConnection(ConfigSettings.ConnectionString))
            {
                using (var command = new SqlCommand("dbo.User_Set", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    var userIDParam = new SqlParameter();
                    userIDParam.ParameterName = "@UserID";
                    userIDParam.SqlDbType = SqlDbType.Int;
                    userIDParam.Value = user.UserID;
                    userIDParam.Direction = ParameterDirection.InputOutput;

                    command.Parameters.Add(userIDParam);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Password_e", user.Password);
                    command.Parameters.AddWithValue("@Salt", user.Salt);
                    command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);

                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();

                    if (user.UserID == null)
                        user.UserID = (int)userIDParam.Value;
                }
            }

            return user.UserID.Value;
        }

        private bool UsernameContainsUnpermittedCharacters(string username)
        {
            return username.Contains(":");
        }

        internal UserAuthentication GetUserAuthenticationObject(int userID)
        {
            var dt = new DataTable();

            using (var conn = new SqlConnection(ConfigSettings.ConnectionString))
            {
                using (var command = new SqlCommand("Auth.User_Authentication_Get", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", userID);

                    using (var adapter = new SqlDataAdapter(command))
                    {
                        conn.Open();
                        adapter.Fill(dt);
                        conn.Close();
                    }
                }
            }

            var row = dt.Rows[0];
            var user = new UserAuthentication();

            user.UserID = (int)row["UserID"];
            user.Password_e = row["Password_e"] as string;
            user.Salt = row["Salt"] as string;
            user.Username = row["Username"] as string;
            return user;
        }
    }
}
