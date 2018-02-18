using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smile.Movies.Dto;

namespace Smile.Movies.Backend.Tests
{
    [TestClass]
    public class SearchingServiceTests
    {
        [TestMethod]
        public void CanCreateSortingService()
        {
            var searcher = new SearchingService();

            Assert.IsNotNull(searcher);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentExceptionIfQueryIsNull()
        {
            var searcher = new SearchingService();

            searcher.Search(null, new List<MovieDto>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentExceptionIfSourceIsNull()
        {
            var searcher = new SearchingService();

            searcher.Search("any", null);
        }

        [TestMethod]
        public void SearchWorksCorrect()
        {
            var searcher = new SearchingService();

            var movies = new List<MovieDto>();

            var movie = new MovieDto();
            movie.Title = "Single responsibility";
            movie.Cast = new List<string> {"NoName"};

            movies.Add(movie);

            movie = new MovieDto();
            movie.Title = "Open/closed principle";
            movie.Cast = new List<string> {"NoName"};

            movies.Add(movie);

            movie = new MovieDto();
            movie.Title = "Liskov substitution principle";
            movie.Cast = new List<string> {"Barbara Liskov"};

            movies.Add(movie);

            movie = new MovieDto();
            movie.Title = "Interface segregation principle";
            movie.Cast = new List<string> {"Bird", "Penguin"};

            movies.Add(movie);

            movie = new MovieDto();
            movie.Title = "Dependency inversion principle";
            movie.Cast = new List<string> {"Abstraction", "NonConcreation"};

            movies.Add(movie);

            var foundMovies = searcher.Search("Barbara Liskov", movies);

            Assert.IsTrue(foundMovies.Count == 1);
            Assert.IsTrue(foundMovies.First().Title == "Liskov substitution principle");
        }
    }
}