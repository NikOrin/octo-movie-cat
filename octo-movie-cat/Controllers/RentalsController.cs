using octo_movie_cat.Contracts;
using octo_movie_cat.Service.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace octo_movie_cat.Controllers
{
    public class RentalsController : ApiController
    {
        [Route("api/rentMovie")]
        [HttpPost]
        public IHttpActionResult RentMovie(RentalRequest request)
        {
            var rentalService = new RentalService();

            if (!rentalService.AuthenticateUser(request.UserID, ActionContext.Request.Headers.Authorization))
                return Unauthorized();
            //authenicate user
            RentalResponse response = rentalService.HandleRequest(request);

            return Ok(response);
        }

        [Route("api/returnMovie")]
        [HttpPost]
        public IHttpActionResult ReturnMovie(int inventoryID)
        {
            //check http authorization header to make sure this is only being called
            //from an internal service that verifies a movie is being returned and not 
            //a user just calling this service

            return Unauthorized();
        }
    }
}
