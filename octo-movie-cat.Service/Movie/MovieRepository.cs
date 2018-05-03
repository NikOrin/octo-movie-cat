using octo_movie_cat.Service.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Service.Movie
{
    public class MovieRepository
    {
        private static MovieRepository _instance;
        public static MovieRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MovieRepository();
                return _instance;
            }
        }

        private MovieRepository() { }

        internal List<MovieEntity> GetMoviesByTitle(string title)
        {
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

            var movies = new List<MovieEntity>();


            foreach (DataRow row in dt.Rows)
            {
                var movie = new MovieEntity();
                movie.MovieID = (int)row["MovieID"];
                movie.Title = row["Title"] as string;
                movie.PhysicalRentalTierID = row["PhysicalRentalTierID"] as byte?;
                movie.SDRentalTierID = (byte)row["SDRentalTierID"];
                movie.HDRentalTierID = (byte)row["HDRentalTierID"];
                movie.InventoryCount = row["InventoryCount"] as int?;
                movie.ReleaseDate = (DateTime)row["ReleaseDate"];
                movie.Description = row["Description"] as string;
                movie.RunTime = row["RunTime"] as byte?;
                movie.MpaaRatingID = row["MpaaRatingID"] as byte?;

                movies.Add(movie);
            }

            return movies;
        }
    }
}
