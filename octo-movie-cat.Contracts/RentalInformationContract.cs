using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Contracts
{
    public class RentalInformationContract
    {
        public int MovieID { get; set; }
        public int MovieName { get; set; }
        public DateTime RentalDate { get; set; }
        public bool Returned { get; set; }
    }
}
