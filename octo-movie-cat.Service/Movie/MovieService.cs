using octo_movie_cat.Contracts;
using octo_movie_cat.Contracts.Enums;
using octo_movie_cat.Service.Common;
using octo_movie_cat.Service.Movie;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Service.Movies
{
    public class MovieService
    {
        private static MovieService _instance;

        public static MovieService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MovieService();
                return _instance;
            }
        }

        public IEnumerable<MovieContract> SearchMovies(string title)
        {
            title = "%" + title + "%";

            var movieEntities = MovieRepository.Instance.GetMoviesByTitle(title);

            var movies = new List<MovieContract>();

            foreach (var movieEntity in movieEntities)
            {
                MovieContract movie = MapEntityToContract(movieEntity);

                movies.Add(movie);
            }

            return movies;
        }

        public void AdvancedSearch(MovieContract movie)
        {

        }

        private MovieContract MapEntityToContract(MovieEntity movieEntity)
        {
            //Consider: using AutoMapper or StructureMap nuget packages for this sort of process
            var movie = new MovieContract();

            movie.MovieID = movieEntity.MovieID;
            movie.Title = movieEntity.Title;
            movie.PhysicalRentalTierID = movieEntity.PhysicalRentalTierID;
            movie.SDRentalTierID = movieEntity.SDRentalTierID;
            movie.HDRentalTierID = movieEntity.HDRentalTierID;
            movie.ReleaseDate = movieEntity.ReleaseDate;
            movie.Description = movieEntity.Description;
            movie.RunTime = movieEntity.RunTime;
            movie.InStock = (movieEntity.InventoryCount ?? 0) > 0;
            movie.MpaaRating = (MpaaRating)movieEntity.MpaaRatingID;

            return movie;
        }
    }
}
