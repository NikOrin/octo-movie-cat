using octo_movie_cat.Contracts;
using octo_movie_cat.Service.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace octo_movie_cat.Controllers
{
    public class MoviesController : ApiController
    {
        [Route("api/search")]
        [HttpGet]
        public IEnumerable<string> SearchSmokeTest()
        {
            return new string[] { "asdf", "test" };
        }

        [Route("api/search/{title}")]
        [HttpGet]
        public IEnumerable<Movie> SearchMovies(string title)
        {
            var movie = new Movie();
            movie.Title = title;
            movie.ReleaseDate = DateTime.Now.Date;

            return new Movie[] { movie };
        }

        [Route("api/PlaceOrder")]
        [HttpPost]
        public RentalResponse PlaceOrder(RentalRequest request)
        {
            if (Request.Headers.Authorization == null) ActionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            else
            {
                var auth = Request.Headers.Authorization;
            }

            //string username = Encoding.UTF8.GetString(Convert.FromBase64String(h))

            var rentalService = new RentalService(request);
            
            return rentalService.DoWork();
        }
    }
}
