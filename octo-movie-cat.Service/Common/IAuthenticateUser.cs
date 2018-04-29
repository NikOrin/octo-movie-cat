using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Service.Common
{
    public interface IAuthenticateUser
    {
        bool AuthenticateUser(int userID, AuthenticationHeaderValue authHeader);
    }
}
