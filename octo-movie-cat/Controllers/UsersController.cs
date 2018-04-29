using octo_movie_cat.Contracts;
using octo_movie_cat.Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace octo_movie_cat.Controllers
{
    public class UsersController : ApiController
    {
        [Route("api/createUser")]
        [HttpPost]
        public IHttpActionResult CreateUser(User user)
        {
            var service = new UserService();
            var userID = service.CreateNewUser(user);
            if (userID == null) return BadRequest();
            return Ok(userID);
        }
    }
}
