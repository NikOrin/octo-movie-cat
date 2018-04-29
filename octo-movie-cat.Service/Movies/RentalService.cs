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
    public class RentalService
    {
        private RentalRequest _request;

        public RentalService(RentalRequest request)
        {
            _request = request;
        }

        public RentalResponse DoWork()
        {
            if (_request == null)
                throw new Exception();

            if (_request.IsPhysicalRental)
                return RentPhysicalCopy();
            else return RentDigitalCopy();

            throw new Exception();
        }

        private RentalResponse RentPhysicalCopy()
        {
            var builder = new SqlConnectionStringBuilder();

            builder.DataSource = ConfigSettings.DatabaseServer;
            builder.InitialCatalog = "Movies";
            builder.IntegratedSecurity = true;

            var dt = new DataTable();

            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                using (var command = new SqlCommand("dbo.Ultimate_Test", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Input", _request.UserID);

                    using (var adapter = new SqlDataAdapter(command))
                    {
                        conn.Open();
                        adapter.Fill(dt);
                        conn.Close();
                    }
                }
            }

            var response = new RentalResponse();
            response.IsSuccess = true;
            response.ConfirmationCode = "AAA" + dt.Rows[0]["Test"].ToString() + dt.Rows[0]["Test2"].ToString();
            return response;
        }

        private RentalResponse RentDigitalCopy()
        {
            throw new NotImplementedException();
        }
    }
}
