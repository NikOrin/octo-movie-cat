using System;

namespace octo_movie_cat.Service.Movie
{
    internal class MovieEntity
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public byte? PhysicalRentalTierID { get; set; }
        public byte SDRentalTierID { get; set; }
        public byte HDRentalTierID { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public byte? RunTime { get; set; }
        public byte? MpaaRatingID { get; set; }
        public int? InventoryCount { get; set; }
    }
}