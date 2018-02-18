using System.Collections.Generic;
using System.Linq;
using Smile.Movies.Backend.Interfaces;
using Smile.Movies.Dto;
using Smile.Movies.Shared;

namespace Smile.Movies.Backend
{
    /// <summary>
    ///     SearchService represents searching logic across all fields within the movie
    /// </summary>
    /// <seealso cref="ISearchingService" />
    public class SearchingService : ISearchingService
    {
        /// <summary>
        ///     Searches the specified query through movies collection.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="movies">The movies.</param>
        /// <returns></returns>
        public List<MovieDto> Search(string query, List<MovieDto> movies)
        {
            Guard.NotNullOrEmpty(query, nameof(query));
            Guard.NotNull(movies, nameof(movies));

            if (string.IsNullOrWhiteSpace(query))
            {
                return movies;
            }

            var result = new List<MovieDto>();

            foreach (var movie in movies)
            {
                if (CheckQuery(query, movie))
                {
                    result.Add(movie);
                }
            }

            return result;
        }

        /// <summary>
        ///     Checks the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="movie">The movie.</param>
        /// <returns>true if match is found otherwise false</returns>
        private bool CheckQuery(string query, MovieDto movie)
        {
            if (!string.IsNullOrWhiteSpace(movie.Classification) &&
                movie.Classification.ToUpper().Contains(query.ToUpper()))
            {
                return true;
            }

            if (!string.IsNullOrWhiteSpace(movie.Genre) && movie.Genre.ToUpper().Contains(query.ToUpper()))
            {
                return true;
            }

            if (!string.IsNullOrWhiteSpace(movie.Title) && movie.Title.ToUpper().Contains(query.ToUpper()))
            {
                return true;
            }

            if (movie.ReleaseDate.ToString().Contains(query))
            {
                return true;
            }

            if (movie.Cast != null && movie.Cast.Any())
            {
                foreach (var cast in movie.Cast)
                {
                    if (!string.IsNullOrWhiteSpace(cast) && cast.ToUpper().Contains(query.ToUpper()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}