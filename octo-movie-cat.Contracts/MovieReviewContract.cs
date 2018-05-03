using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Contracts
{
    public class MovieReviewContract
    {
        public int MovieID { get; set; }
        public int UserID { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public byte Rating { get; set; }
        public string Review { get; set; }
    }
}
