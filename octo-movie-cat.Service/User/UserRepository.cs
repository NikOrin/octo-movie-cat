using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using octo_movie_cat.Contracts;
using octo_movie_cat.Service.Common;

namespace octo_movie_cat.Service.User
{
    public class UserRepository
    {
        private static UserRepository _instance;

        public static UserRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserRepository();
                return _instance;
            }
        }

        private UserRepository() { }

        public int CreateNewUser(UserEntity user)
        {
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
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Password_e", user.Password);
                    command.Parameters.AddWithValue("@Salt", user.Salt);
                    command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);

                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();

                    user.UserID = (int)userIDParam.Value;
                }
            }
            return user.UserID.Value;
        }

        public UserEntity GetUser(int userID)
        {
            var dt = new DataTable();

            using (var conn = new SqlConnection(ConfigSettings.ConnectionString))
            {
                using (var command = new SqlCommand("dbo.User_Get", conn))
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

            DataRow row = dt.Rows[0];
            var user = new UserEntity();
            user.UserID = (int)row["UserID"];
            user.Username = row["Username"] as string;
            user.Email = row["Email"] as string;
            user.FirstName = row["FirstName"] as string;
            user.LastName = row["LastName"] as string;
            user.DateOfBirth = (DateTime)row["DateOfBirth"];
            user.CreatedOn = (DateTime)row["CreatedOn"];
            user.UpdatedOn = (DateTime)row["UpdatedOn"];

            return user;
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
