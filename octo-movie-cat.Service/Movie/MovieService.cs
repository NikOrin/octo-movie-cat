using octo_movie_cat.Contracts;
using octo_movie_cat.Service.Common;
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

            var movies = new List<MovieContract>();
            var dt = new DataTable();
            using (var conn = new SqlConnection(ConfigSettings.ConnectionString))
            {
                using (var command = new SqlCommand("dbo.Movie_GetByTitle", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Title", title);

                    using (var adapter = new SqlDataAdapter(command))
                    {
                        conn.Open();
                        adapter.Fill(dt);
                        conn.Close();
                    }
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                var movie = new MovieContract();
                movie.MovieID = (int)row["MovieID"];
                movie.Title = row["Title"] as string;
                movie.PhysicalRentalTierID = row["PhysicalRentalTierID"] as byte?;
                movie.SDRentalTierID = (byte)row["SDRentalTierID"];
                movie.HDRentalTierID = (byte)row["HDRentalTierID"];
                var inventoryCount = row["InventoryCount"] as int?;

                movie.InStock = inventoryCount > 0;
                movie.ReleaseDate = (DateTime)row["ReleaseDate"];
                movie.Description = row["Description"] as string;

                movies.Add(movie);
            }

            return movies;
        }

        public void AdvancedSearch(MovieContract movie)
        {
        }
    }
}
