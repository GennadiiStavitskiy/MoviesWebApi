using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smile.Movies.Backend.Interfaces;
using Smile.Movies.Dto;
using Smile.Movies.Shared;

namespace Smile.Movies.Backend.Tests
{
    [TestClass]
    public class CachingServiceTests
    {
        private ICachingService _cashingService;

        [TestInitialize]
        public void TestInitialize()
        {
            IMemoryCache cash = new MemoryCache(Options.Create(new MemoryCacheOptions()));
            _cashingService =
                new CachingService(cash, Options.Create(new MemoryCacheSettings {ExpirationTimeHours = 1}));
        }


        [TestMethod]
        public void GetMovieByIdReturnMovieFromAllMoviesList()
        {
            var movie = new MovieDto();
            movie.MovieId = 5;
            movie.Title = "First_5";

            _cashingService.SetMovie(movie);

            var movie_second_5 = new MovieDto();
            movie_second_5.MovieId = 5;
            movie_second_5.Title = "Second_5";

            var movies = new List<MovieDto> {movie_second_5};

            _cashingService.SetAllMovies(movies);

            var movieFromCash = _cashingService.GetMovieById(movie.MovieId);

            Assert.AreNotSame(movie, movieFromCash);

            Assert.AreSame(movie_second_5, movieFromCash);
        }

        [TestMethod]
        public void GetMovieByIdReturnMovieFromCache()
        {
            var movie = new MovieDto();
            movie.MovieId = 5;
            movie.Title = "First_5";

            _cashingService.SetMovie(movie);

            var movieFromCash = _cashingService.GetMovieById(movie.MovieId);

            Assert.AreSame(movie, movieFromCash);
        }
    }
}