using octo_movie_cat.Contracts;
using octo_movie_cat.Service.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Service.Movies
{
    public class RentalService : IAuthenticateUser
    {
        public RentalResponse HandleRequest(RentalRequest request)
        {
            if (request == null)
                throw new Exception();
            var response = new RentalResponse();
                
            byte rentalDurationHours = GetRentalDuration(request);

            Int64? rentalID;
           
            using (var conn = new SqlConnection(ConfigSettings.ConnectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    int? inventoryID = null;
                    if (request.IsPhysicalRental)
                    {
                        inventoryID = GetInventoryID(request.MovieID, conn, transaction);

                        if (inventoryID == null)//out of stock
                        {
                            throw new Exception("Movie out of stock. Could not fulfill physical rental request");
                        }
                    }

                    using (var command = new SqlCommand("dbo.RentMovie", conn, transaction))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@MovieID", request.MovieID);
                        command.Parameters.AddWithValue("@InventoryID", inventoryID);
                        command.Parameters.AddWithValue("@RentalDurationHours", rentalDurationHours);

                        var outputParameter = new SqlParameter();
                        outputParameter.DbType = DbType.Int64;
                        outputParameter.ParameterName = "@RentalID";
                        outputParameter.Direction = ParameterDirection.Output;

                        command.Parameters.Add(outputParameter);

                        command.ExecuteNonQuery();

                        rentalID = outputParameter.Value as Int64?;

                        if (rentalID == null)
                            throw new Exception("Rental process failed");
                        else
                        {
                            response.IsSuccess = true;
                            response.ConfirmationID = rentalID;
                        }
                    }
                    transaction.Commit();
                    conn.Close();
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    conn.Close();
                    response.IsSuccess = false;
                    response.Message = ex.Message;
                }
                
            }
            
            return response;
        }

        private byte GetRentalDuration(RentalRequest request)
        {
            //physical rentals should be a week long, whereas digital is 2 days
            //We can replace this logic with maybe user defined lengths
            //or a database call to read from a type-table and see various lengths based on movie type
            if (request.IsPhysicalRental)
                return 7 * 24;
            else return 48;
        }

        private int? GetInventoryID(int movieID, SqlConnection conn, SqlTransaction transaction)
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

        public int AuthenticateUser(string username, string password)
        {
            return 0;
        }
    }
}
