using System.Collections.Generic;
using System.Linq;
using Smile.Movies.Backend.Interfaces;
using Smile.Movies.Dto;
using Smile.Movies.Shared;

namespace Smile.Movies.Backend
{
    /// <summary>
    ///     DataService encapsulates logic related to fetching, adding, updating data
    /// </summary>
    /// <seealso cref="Smile.Movies.Backend.Interfaces.IDataService" />
    public class DataService : IDataService
    {
        private readonly ICachingService _cachingService;

        private readonly IDataSourceAdapter _dataSource;

        private readonly ISearchingService _searchingService;

        private readonly ISortingService _sorter;

        public DataService(IDataSourceAdapter dataSource, ISortingService sorter,
            ISearchingService searcher, ICachingService cachingService)
        {
            _dataSource = dataSource;

            _cachingService = cachingService;

            _sorter = sorter;

            _searchingService = searcher;
        }

        /// <summary>
        ///     Creates the movie.
        /// </summary>
        /// <param name="movie">The movie.</param>
        /// <returns>MovieDto</returns>
        public MovieDto CreateMovie(MovieDto movie)
        {
            movie.MovieId = _dataSource.CreateMovie(movie);

            if (movie.MovieId > 0)
            {
                _cachingService.SetMovie(movie);

                return movie;
            }

            return null;
        }

        /// <summary>
        ///     Gets all movies.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns>List of MovieDto</returns>
        public List<MovieDto> GetAllMovies(string searchQuery, string sortExpression)
        {
            var result = GetAllMoviesFromCashOrDataSource();

            // search
            if (!string.IsNullOrWhiteSpace(searchQuery) && result != null && result.Any())
            {
                result = _searchingService.Search(searchQuery, result);
            }

            //sort
            if (!string.IsNullOrWhiteSpace(sortExpression) && result != null && result.Any())
            {
                result = _sorter.Sort(sortExpression, result);
            }

            return result;
        }

        /// <summary>
        ///     Gets the movie by identifier.
        /// </summary>
        /// <param name="movieId">The movie identifier.</param>
        /// <returns>MovieDto</returns>
        public MovieDto GetMovieById(int movieId)
        {
            Guard.IntMoreThanZero(movieId, nameof(movieId));

            MovieDto movie = null;

            var allMovies = _cachingService.GetAllMovies();

            if (allMovies != null)
            {
                movie = allMovies.FirstOrDefault(m => m.MovieId == movieId);
            }

            if (movie == null)
            {
                movie = _cachingService.GetMovieById(movieId);
            }

            if (movie == null)
            {
                movie = _dataSource.GetMovieById(movieId);

                _cachingService.SetMovie(movie);
            }

            return movie;
        }

        /// <summary>
        ///     Updates the movie.
        /// </summary>
        /// <param name="movie">The movie.</param>
        public void UpdateMovie(MovieDto movie)
        {
            Guard.NotNull(movie, nameof(movie));

            _dataSource.UpdateMovie(movie);

            _cachingService.SetMovie(movie);
        }

        private List<MovieDto> GetAllMoviesFromCashOrDataSource()
        {
            var allMovies = _cachingService.GetAllMovies();

            if (allMovies == null)
            {
                allMovies = _dataSource.GetAllMovies();

                _cachingService.SetAllMovies(allMovies);
            }

            return allMovies;
        }
    }
}