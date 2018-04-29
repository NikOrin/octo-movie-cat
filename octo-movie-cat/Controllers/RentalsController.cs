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
            //authenicate user
            return Ok(rentalService.HandleRequest(request));
        }
    }
}
