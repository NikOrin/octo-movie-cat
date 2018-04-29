using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Contracts
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public byte? PhysicalRentalTierID { get; set; } 
        public byte SDRentalTierID { get; set; }
        public byte HDRentalTierID { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public bool InStock { get; set; }
        public bool DigitalRentalOnly
        {
            get
            {
                return !PhysicalRentalTierID.HasValue;
            }
        }
    }
}
