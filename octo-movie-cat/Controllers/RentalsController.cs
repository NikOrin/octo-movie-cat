using octo_movie_cat.Contracts;
using octo_movie_cat.Service.Rental;
using System.Web.Http;

namespace octo_movie_cat.Controllers
{
    public class RentalsController : ApiController
    {
        [Route("api/rentMovie")]
        [HttpPost]
        public IHttpActionResult RentMovie(RentalRequestContract request)
        {
            if (!RentalService.Instance.AuthenticateUser(request.UserID, ActionContext.Request?.Headers?.Authorization))
                return Unauthorized();
            //authenicate user
            RentalResponseContract response = RentalService.Instance.HandleRentalRequest(request);

            return Ok(response);
        }

        [Route("api/returnMovie/{inventoryID}")]
        [HttpPost]
        public IHttpActionResult ReturnPhysicalMovie(int inventoryID)
        {
            //check http authorization header to make sure this is only being called
            //from an internal service that verifies a movie is being returned and not 
            //a user just calling this service
            bool returnSuccessful = RentalService.Instance.ReturnMovie(inventoryID);

            if (returnSuccessful)
                return Ok("Movie successfully returned");
            else return BadRequest("Something went wrong during return process");
        }

        [Route("api/revokeOnlineRental/{rentalID}")]
        [HttpPost]
        public IHttpActionResult RevokeOnlineRental(int rentalID)
        {
            bool revokeSuccessful = RentalService.Instance.RevokeOnlineRental(rentalID);

            if (revokeSuccessful)
                return Ok();
            else return BadRequest();
        }
    }
}
