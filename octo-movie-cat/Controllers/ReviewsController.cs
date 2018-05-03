using octo_movie_cat.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace octo_movie_cat.Controllers
{
    public class ReviewsController : ApiController
    {
        [Route("api/submitReview")]
        [HttpPost]
        public IHttpActionResult SubmitReview(MovieReviewContract review)
        {

            return Ok();
        }
    }
}
