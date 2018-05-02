using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Contracts
{
    public class UserDetailsContract : UserContract
    {
        public List<MovieReviewContract> Reviews { get; set; }
        public List<RentalInformationContract> RentalHistory { get; set; }
    }
}
