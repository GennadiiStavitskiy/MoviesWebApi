using System.Collections.Generic;
using Smile.Movies.Dto;

namespace Smile.Movies.Backend.Interfaces
{
    /// <summary>
    ///     IDataSourceAdapter represents signatures for particular DataSourceAdapter instance class
    /// </summary>
    public interface IDataSourceAdapter
    {
        /// <summary>
        ///     Gets all movies.
        /// </summary>
        /// <returns></returns>
        List<MovieDto> GetAllMovies();

        /// <summary>
        ///     Gets the movie by identifier.
        /// </summary>
        /// <param name="movieId">The movie identifier.</param>
        /// <returns></returns>
        MovieDto GetMovieById(int movieId);

        /// <summary>
        ///     Creates the movie.
        /// </summary>
        /// <param name="movie">The movie.</param>
        /// <returns></returns>
        int CreateMovie(MovieDto movie);

        /// <summary>
        ///     Updates the movie.
        /// </summary>
        /// <param name="movie">The movie.</param>
        void UpdateMovie(MovieDto movie);
    }
}