using System.Collections.Generic;
using System.Linq;
using MoviesLibrary;
using Smile.Movies.Backend.Extentions;
using Smile.Movies.Backend.Interfaces;
using Smile.Movies.Dto;
using Smile.Movies.Shared;

namespace Smile.Movies.Backend
{
    /// <summary>
    ///     DataSourceAdapter wraps MovieDataSorurce from MoviesLibrary
    /// </summary>
    /// <seealso cref="IDataSourceAdapter" />
    public class DataSourceAdapter : IDataSourceAdapter
    {
        private readonly MovieDataSource _dataSource;

        public DataSourceAdapter()
        {
            _dataSource = new MovieDataSource();
        }

        /// <summary>
        ///     Gets all movies from data source.
        /// </summary>
        /// <returns>List of MovieDto</returns>
        public List<MovieDto> GetAllMovies()
        {
            var movies = _dataSource.GetAllData();

            return movies.Select(m => m.ToDto()).ToList();
        }

        /// <summary>
        ///     Gets the movie by identifier.
        /// </summary>
        /// <param name="movieId">The movie identifier.</param>
        /// <returns>MovieDto</returns>
        public MovieDto GetMovieById(int movieId)
        {
            var movie = _dataSource.GetDataById(movieId);

            return movie?.ToDto();
        }

        /// <summary>
        ///     Creates the movie.
        /// </summary>
        /// <param name="movie">The movie.</param>
        /// <returns>movie id</returns>
        public int CreateMovie(MovieDto movie)
        {
            Guard.NotNull(movie, nameof(movie));

            return _dataSource.Create(movie.ToMovieData());
        }

        /// <summary>
        ///     Updates the movie.
        /// </summary>
        /// <param name="movie">The movie.</param>
        public void UpdateMovie(MovieDto movie)
        {
            Guard.NotNull(movie, nameof(movie));
            _dataSource.Update(movie.ToMovieData());
        }
    }
}