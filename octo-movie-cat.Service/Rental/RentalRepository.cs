using octo_movie_cat.Service.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Service.Rental
{
    public class RentalRepository
    {
        private static RentalRepository _instance;

        public static RentalRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RentalRepository();
                return _instance;
            }
        }

        private RentalRepository() { }

        public int? CheckoutMovie(int movieID, SqlConnection conn, SqlTransaction transaction)
        {
            int? inventoryID;
            using (var command = new SqlCommand("dbo.Inventory_Checkout", conn, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@MovieID", movieID);

                var outputParameter = new SqlParameter();
                outputParameter.ParameterName = "@InventoryID";
                outputParameter.DbType = DbType.Int32;
                outputParameter.Direction = ParameterDirection.Output;

                command.Parameters.Add(outputParameter);

                command.ExecuteNonQuery();

                inventoryID = outputParameter.Value as int?;
            }

            return inventoryID;
        }

        internal long? RentMovie(RentalEntity rental, SqlConnection conn, SqlTransaction transaction)
        {
            long? rentalID;
            using (var command = new SqlCommand("dbo.RentMovie", conn, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", rental.UserID);
                command.Parameters.AddWithValue("@MovieID", rental.MovieID);
                command.Parameters.AddWithValue("@InventoryID", rental.InventoryID);
                command.Parameters.AddWithValue("@RentalDurationHours", rental.RentalDurationHours);

                var outputParameter = new SqlParameter();
                outputParameter.DbType = DbType.Int64;
                outputParameter.ParameterName = "@RentalID";
                outputParameter.Direction = ParameterDirection.Output;

                command.Parameters.Add(outputParameter);

                command.ExecuteNonQuery();

                rentalID = outputParameter.Value as Int64?;
            }

            return rentalID;
        }

        public long? GetRentalID(int inventoryID)
        {
            long? rentalID;
            using (var conn = new SqlConnection(ConfigSettings.ConnectionString))
            {
                using (var command = new SqlCommand("dbo.Rental_RentalID_Get", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@InventoryID", inventoryID);

                    var outputParameter = new SqlParameter();
                    outputParameter.DbType = DbType.Int64;
                    outputParameter.ParameterName = "@RentalID";
                    outputParameter.Direction = ParameterDirection.Output;

                    command.Parameters.Add(outputParameter);

                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();

                    rentalID = outputParameter.Value as long?;
                }
            }
            return rentalID;
        }

        public int ReturnMovie(long rentalID, bool isPhysicalReturn)
        {
            int rowsAffected;
            using (var conn = new SqlConnection(ConfigSettings.ConnectionString))
            {
                using (var command = new SqlCommand("dbo.ReturnMovie", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RentalID", rentalID);
                    command.Parameters.AddWithValue("@IsPhysicalReturn", isPhysicalReturn);

                    var rowsAffectedParameter = new SqlParameter();
                    rowsAffectedParameter.ParameterName = "@RowsAffected";
                    rowsAffectedParameter.Direction = ParameterDirection.Output;
                    rowsAffectedParameter.SqlDbType = SqlDbType.Int;

                    command.Parameters.Add(rowsAffectedParameter);

                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();

                    rowsAffected = (int)rowsAffectedParameter.Value;
                }
            }
            return rowsAffected;
        }
    }
}
