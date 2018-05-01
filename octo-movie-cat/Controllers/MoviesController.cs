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
        [Route("api/search/{title}")]
        [HttpGet]
        public IHttpActionResult SearchMovies(string title)
        {
            var movieService = new MovieService();
            return Ok(movieService.SearchMovies(title));
        }
    }
}
