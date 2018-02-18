using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smile.Movies.Backend.SortingStrategies;
using Smile.Movies.Dto;
using Smile.Movies.Dto.Enums;

namespace Smile.Movies.Backend.Tests.SortingStrategies
{
    [TestClass]
    public class TitleSortingStrategyTests
    {
        private readonly List<MovieDto> movies = new List<MovieDto>();

        [TestInitialize]
        public void TestInitialize()
        {
            var movie = new MovieDto();
            movie.Title = "D";
            movie.Cast = new List<string> {"NoName"};

            movies.Add(movie);

            movie = new MovieDto();
            movie.Title = "E";
            movie.Cast = new List<string> {"NoName"};

            movies.Add(movie);

            movie = new MovieDto();
            movie.Title = "C";
            movie.Cast = new List<string> {"Barbara Liskov"};

            movies.Add(movie);

            movie = new MovieDto();
            movie.Title = "A";
            movie.Cast = new List<string> {"Bird", "Penguin"};

            movies.Add(movie);

            movie = new MovieDto();
            movie.Title = "B";
            movie.Cast = new List<string> {"Abstraction", "NonConcreation"};

            movies.Add(movie);
        }


        [TestMethod]
        public void TitleSortingStrategyAscendingSortCorrect()
        {
            var strategy = new TitleSortingStrategy();

            var sortedMovies = strategy.OrderBy(SortingDirections.Ascending, movies);

            Assert.IsTrue(sortedMovies.First().Title == "A");
            Assert.IsTrue(sortedMovies.Last().Title == "E");
        }

        [TestMethod]
        public void TitleSortingStrategyDescendingSortCorrect()
        {
            var strategy = new TitleSortingStrategy();

            var sortedMovies = strategy.OrderBy(SortingDirections.Descending, movies);

            Assert.IsTrue(sortedMovies.First().Title == "E");
            Assert.IsTrue(sortedMovies.Last().Title == "A");
        }
    }
}