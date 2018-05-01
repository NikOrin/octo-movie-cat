using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Service.Common
{
    public class BaseService : IAuthenticateUser
    {
        public bool AuthenticateUser(int userID, AuthenticationHeaderValue authHeader)
        {
            byte[] data = Convert.FromBase64String(authHeader.Parameter);
            string[] authHeaderRaw = Encoding.UTF8.GetString(data).Split(new char[] { ':' });

            return Security.AuthenticateUser(userID, authHeaderRaw[0], authHeaderRaw[1]);
        }
    }
}
