using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Service.Rental
{
    public class RentalEntity
    {
        public long? RentalID;
        public int UserID;
        public int MovieID;
        public int InventoryID;
        public byte RentalDurationHours;
    }
}
