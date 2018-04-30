﻿using octo_movie_cat.Contracts;
using octo_movie_cat.Service.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Service.Rental
{
    public class RentalService : IAuthenticateUser
    {
        private static RentalService _instance;

        public static RentalService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RentalService();
                return _instance;
            }
        }

        private RentalService()
        {

        }

        public RentalResponse HandleRentalRequest(RentalRequest request)
        {
            if (request == null)
                throw new Exception();

            var response = new RentalResponse();
                
            byte rentalDurationHours = GetRentalDuration(request);

            long? rentalID;
           
            using (var conn = new SqlConnection(ConfigSettings.ConnectionString))
            {
                conn.Open();
                //transaction necessary to make sure multiple don't checkout the same dvd/vhs
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    int? inventoryID = null;
                    if (request.IsPhysicalRental)
                    {
                        inventoryID = RentalRepository.Instance.CheckoutMovie(request.MovieID, conn, transaction);

                        if (inventoryID == null)//out of stock
                        {
                            throw new Exception("Movie out of stock. Could not fulfill physical rental request");
                        }
                    }

                    var rental = new RentalEntity();
                    rental.MovieID = request.MovieID;
                    rental.UserID = request.UserID;
                    rental.InventoryID = inventoryID.Value;
                    rental.RentalDurationHours = rentalDurationHours;

                    rentalID = RentalRepository.Instance.RentMovie(rental, conn, transaction);

                    if (rentalID == null)
                        throw new Exception("Something went wrong during movie checkout");

                    response.IsSuccess = true;
                    response.ConfirmationID = rentalID;

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

        public bool ReturnMovie(int inventoryID)
        {
            int rowsAffected;

            long rentalID = RentalRepository.Instance.GetRentalID(inventoryID);

            rowsAffected = RentalRepository.Instance.ReturnMovie(rentalID, true);

            return rowsAffected == 1;
        }

        public bool RevokeOnlineRental(int rentalID)
        {
            int rowsAffected;

            rowsAffected = RentalRepository.Instance.ReturnMovie(rentalID, false);

            return rowsAffected == 1;
        }

        public bool AuthenticateUser(int userID, AuthenticationHeaderValue authHeader)
        {
            byte[] data = Convert.FromBase64String(authHeader.Parameter);
            string[] authHeaderRaw = Encoding.UTF8.GetString(data).Split( new char[] { ':' });

            return Security.AuthenticateUser(userID, authHeaderRaw[0], authHeaderRaw[1]);
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
    }
}
