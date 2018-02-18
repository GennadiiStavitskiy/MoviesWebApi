using System.Collections.Generic;
using Smile.Movies.Dto;

namespace Smile.Movies.Backend.Interfaces
{
    /// <summary>
    ///     IDataService represent signatures for particular
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        ///     Gets all movies.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        List<MovieDto> GetAllMovies(string searchQuery, string sortExpression);

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
        MovieDto CreateMovie(MovieDto movie);

        /// <summary>
        ///     Updates the movie.
        /// </summary>
        /// <param name="movie">The movie.</param>
        void UpdateMovie(MovieDto movie);
    }
}