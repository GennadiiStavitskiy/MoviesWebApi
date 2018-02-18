using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Smile.Movies.Backend.Interfaces;
using Smile.Movies.Dto;
using Smile.Movies.Shared;

namespace Smile.Movies.Backend
{
    /// <summary>
    ///     CachingService includes a logic related to caching movies to MemoryCache
    /// </summary>
    /// <seealso cref="ICachingService" />
    public class CachingService : ICachingService
    {
        public const string MovieIdCacheKey = "MovieId_";

        public const string AllMoviesCacheKey = "AllMoviesCacheKey";

        private readonly IMemoryCache _cache;

        private readonly MemoryCacheSettings _cacheSettings;

        public CachingService(IMemoryCache memoryCache, IOptions<MemoryCacheSettings> cacheSettings)
        {
            _cache = memoryCache;
            _cacheSettings = cacheSettings.Value;
        }

        /// <summary>
        ///     Gets all movies from cache.
        /// </summary>
        /// <returns>List of MovieDto objects</returns>
        public List<MovieDto> GetAllMovies()
        {
            return _cache.Get<List<MovieDto>>(AllMoviesCacheKey);
        }

        /// <summary>
        ///     Gets the movie by identifier from cache.
        /// </summary>
        /// <param name="movieId">The movie identifier.</param>
        /// <returns>MovieDto</returns>
        public MovieDto GetMovieById(int movieId)
        {
            MovieDto movie = null;

            var allMovies = _cache.Get<List<MovieDto>>(AllMoviesCacheKey);

            if (allMovies != null)
            {
                movie = allMovies.FirstOrDefault(m => m.MovieId == movieId);
            }

            if (movie == null)
            {
                movie = _cache.Get<MovieDto>(MovieIdCacheKey + movieId);
            }

            return movie;
        }

        /// <summary>
        ///     Sets all movies to cache.
        /// </summary>
        /// <param name="movies">The movies.</param>
        public void SetAllMovies(List<MovieDto> movies)
        {
            Guard.ListNotNullOrEmpty(movies, nameof(movies));

            _cache.Set(AllMoviesCacheKey, movies,
                new DateTimeOffset(DateTime.Now.AddHours(_cacheSettings.ExpirationTimeHours)));
        }

        /// <summary>
        ///     Sets the movie to cache.
        /// </summary>
        /// <param name="movie">The movie.</param>
        public void SetMovie(MovieDto movie)
        {
            Guard.NotNull(movie, nameof(movie));

            var allMovies = _cache.Get<List<MovieDto>>(AllMoviesCacheKey);

            if (allMovies != null)
            {
                var index = allMovies.FindIndex(m => m.MovieId == movie.MovieId);

                if (index > 0)
                {
                    allMovies[index] = movie;
                }
                else
                {
                    allMovies.Add(movie);
                }

                _cache.Remove(MovieIdCacheKey + movie.MovieId);
            }
            else
            {
                _cache.Set(MovieIdCacheKey + movie.MovieId, movie,
                    new DateTimeOffset(DateTime.Now.AddHours(_cacheSettings.ExpirationTimeHours)));
            }
        }
    }
}