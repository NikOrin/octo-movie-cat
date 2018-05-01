using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Contracts
{
    public class RentalRequestContract
    {
        public int UserID;
        public int MovieID;
        public bool IsPhysicalRental;
    }
}
