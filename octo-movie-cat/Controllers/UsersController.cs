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
        public IHttpActionResult CreateUser(UserContract user)
        {
            var userID = UserService.Instance.CreateNewUser(user);
            if (userID == null) return BadRequest("Something went wrong during user creation");
            return Ok(userID);
        }

        [Route("api/getUser/{userID}")]
        [HttpGet]
        public IHttpActionResult GetUser(int userID)
        {
            if (!UserService.Instance.AuthenticateUser(userID, ActionContext.Request.Headers.Authorization))
                return Unauthorized();

            var user = UserService.Instance.GetUser(userID);

            return Ok(user);
        }

        [Route("api/getUserDetails/{userID}")]
        [HttpGet]
        public IHttpActionResult GetUserDetails(int userID)
        {
            //used to get order history and review history
            if (!UserService.Instance.AuthenticateUser(userID, ActionContext.Request.Headers.Authorization))
                return Unauthorized();

            var userDetails = UserService.Instance.GetUserDetails(userID);

            return Ok(userDetails);
        }
    }
}
