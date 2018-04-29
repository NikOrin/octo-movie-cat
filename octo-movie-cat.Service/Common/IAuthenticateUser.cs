using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Service.Common
{
    public interface IAuthenticateUser
    {
        int AuthenticateUser(string username, string password);
    }
}
