using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smile.Movies.Backend.SortingStrategies;
using Smile.Movies.Dto;
using Smile.Movies.Dto.Enums;

namespace Smile.Movies.Backend.Tests.SortingStrategies
{
    [TestClass]
    public class ReleaseDateSortingStrategyTests
    {
        private readonly List<MovieDto> movies = new List<MovieDto>();

        [TestInitialize]
        public void TestInitialize()
        {
            var movie = new MovieDto();
            movie.ReleaseDate = 1980;
            movie.Cast = new List<string> {"NoName"};

            movies.Add(movie);

            movie = new MovieDto();
            movie.ReleaseDate = 1979;
            movie.Cast = new List<string> {"NoName"};

            movies.Add(movie);

            movie = new MovieDto();
            movie.ReleaseDate = 2000;
            movie.Cast = new List<string> {"Barbara Liskov"};

            movies.Add(movie);

            movie = new MovieDto();
            movie.ReleaseDate = 2017;
            movie.Cast = new List<string> {"Bird", "Penguin"};

            movies.Add(movie);

            movie = new MovieDto();
            movie.ReleaseDate = 2013;
            movie.Cast = new List<string> {"Abstraction", "NonConcreation"};

            movies.Add(movie);
        }

        [TestMethod]
        public void ReleaseDateSortingStrategyAscendingSortCorrect()
        {
            var strategy = new ReleaseDateSortingStrategy();

            var sortedMovies = strategy.OrderBy(SortingDirections.Ascending, movies);

            Assert.IsTrue(sortedMovies.First().ReleaseDate == 1979);
            Assert.IsTrue(sortedMovies.Last().ReleaseDate == 2017);
        }

        [TestMethod]
        public void ReleaseSortingStrategyDescendingSortCorrect()
        {
            var strategy = new ReleaseDateSortingStrategy();

            var sortedMovies = strategy.OrderBy(SortingDirections.Descending, movies);

            Assert.IsTrue(sortedMovies.First().ReleaseDate == 2017);
            Assert.IsTrue(sortedMovies.Last().ReleaseDate == 1979);
        }
    }
}