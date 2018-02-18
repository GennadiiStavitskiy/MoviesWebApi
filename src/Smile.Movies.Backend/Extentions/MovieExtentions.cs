using System.Collections.Generic;
using MoviesLibrary;
using Smile.Movies.Dto;

namespace Smile.Movies.Backend.Extentions
{
    /// <summary>
    ///     MovieExtentions represents extentions for MovieData and MovieDto classes
    /// </summary>
    public static class MovieExtentions
    {
        /// <summary>
        ///     Convert the MovieData object to MovieDto object.
        /// </summary>
        /// <param name="movie">The movie.</param>
        /// <returns>MovieDto</returns>
        public static MovieDto ToDto(this MovieData movie)
        {
            var movieDto = new MovieDto
            {
                Cast = new List<string>(movie.Cast),
                Classification = movie.Classification,
                Genre = movie.Genre,
                MovieId = movie.MovieId,
                Rating = movie.Rating,
                ReleaseDate = movie.ReleaseDate,
                Title = movie.Title
            };

            return movieDto;
        }

        /// <summary>
        ///     Convert the MovieDto object to MovieData object.
        /// </summary>
        /// <param name="movie">The movie.</param>
        /// <returns>MovieData</returns>
        public static MovieData ToMovieData(this MovieDto movie)
        {
            var movieData = new MovieData
            {
                Cast = movie.Cast.ToArray(),
                Classification = movie.Classification,
                Genre = movie.Genre,
                MovieId = movie.MovieId,
                Rating = movie.Rating,
                ReleaseDate = movie.ReleaseDate,
                Title = movie.Title
            };

            return movieData;
        }
    }
}