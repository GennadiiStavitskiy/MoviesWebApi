using System.Collections.Generic;
using Smile.Movies.Dto;

namespace Smile.Movies.Backend.Interfaces
{
    /// <summary>
    ///     ICachingService represent signatures for particular Caching Service
    /// </summary>
    public interface ICachingService
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
        ///     Sets all movies.
        /// </summary>
        /// <param name="movies">The movies.</param>
        void SetAllMovies(List<MovieDto> movies);

        /// <summary>
        ///     Sets the movie.
        /// </summary>
        /// <param name="movie">The movie.</param>
        void SetMovie(MovieDto movie);
    }
}