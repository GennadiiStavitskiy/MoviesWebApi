using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Smile.Movies.Backend.Interfaces;
using Smile.Movies.Backend.SortingStrategies;
using Smile.Movies.Dto;
using Smile.Movies.Dto.Enums;

namespace Smile.Movies.Backend.Tests
{
    [TestClass]
    public class SortingServiceTests
    {
        [TestMethod]
        public void CanCreateSortingService()
        {
            var factory = Mock.Of<ISortingStrategyFactory>();

            var sorter = new SortingService(factory);

            Assert.IsNotNull(sorter);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentExceptionIfSortByIsNull()
        {
            var factory = Mock.Of<ISortingStrategyFactory>();

            var sorter = new SortingService(factory);

            sorter.Sort(null, new List<MovieDto>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentExceptionIfSourceIsNull()
        {
            var factory = Mock.Of<ISortingStrategyFactory>();

            var sorter = new SortingService(factory);

            sorter.Sort("any", null);
        }

        [TestMethod]
        public void AscendingSortPerformed()
        {
            var movies = new List<MovieDto>();

            var movie = new MovieDto();
            movie.Title = "B";
            movie.Cast = new List<string> {"Bird", "Penguin"};

            movies.Add(movie);

            movie = new MovieDto();
            movie.Title = "A";
            movie.Cast = new List<string> {"Abstraction", "NonConcreation"};

            movies.Add(movie);

            var factory = Mock.Of<ISortingStrategyFactory>();
            var strategy = Mock.Of<ISortingStrategy>();
            Mock.Get(factory).Setup(f => f.Get(SortingItems.Title)).Returns(strategy);

            var sorter = new SortingService(factory);

            sorter.Sort("title", movies);

            Mock.Get(factory).Verify(f => f.Get(SortingItems.Title), Times.Once);
            Mock.Get(strategy).Verify(s => s.OrderBy(SortingDirections.Ascending, movies), Times.Once);
        }

        [TestMethod]
        public void DescendingSortExecuted()
        {
            var movies = new List<MovieDto>();

            var movie = new MovieDto();
            movie.Title = "B";
            movie.Cast = new List<string> {"Bird", "Penguin"};

            movies.Add(movie);

            movie = new MovieDto();
            movie.Title = "A";
            movie.Cast = new List<string> {"Abstraction", "NonConcreation"};

            movies.Add(movie);

            var factory = Mock.Of<ISortingStrategyFactory>();
            var strategy = Mock.Of<ISortingStrategy>();
            Mock.Get(factory).Setup(f => f.Get(SortingItems.Title)).Returns(strategy);

            var sorter = new SortingService(factory);

            sorter.Sort("-title", movies);

            Mock.Get(factory).Verify(f => f.Get(SortingItems.Title), Times.Once);
            Mock.Get(strategy).Verify(s => s.OrderBy(SortingDirections.Descending, movies), Times.Once);
        }

        [TestMethod]
        public void MultiFieldsSortingWithAscendingOrderCorrect()
        {
            var movies = new List<MovieDto>();

            var movieB17 = new MovieDto();
            movieB17.Title = "B";
            movieB17.MovieId = 17;
            movies.Add(movieB17);

            var movieB15 = new MovieDto();
            movieB15.Title = "B";
            movieB15.MovieId = 15;
            movies.Add(movieB15);

            var movieAb = new MovieDto();
            movieAb.Title = "AB";
            movieAb.MovieId = 10;
            movies.Add(movieAb);

            var movieAa2 = new MovieDto();
            movieAa2.Title = "AA";
            movieAa2.MovieId = 2;
            movies.Add(movieAa2);

            var movieAa1 = new MovieDto();
            movieAa1.Title = "AA";
            movieAa1.MovieId = 1;
            movies.Add(movieAa1);

            var movieAa3 = new MovieDto();
            movieAa3.Title = "AA";
            movieAa3.MovieId = 3;
            movies.Add(movieAa3);


            var sorter = new SortingService(new SortingStrategyFactory());

            movies = sorter.Sort("title,movieid", movies);

            Assert.IsTrue(movies.First() == movieAa1);
            Assert.IsTrue(movies.Last() == movieB17);
        }

        [TestMethod]
        public void MultiFieldsSortingWithDescendingOrderCorrect()
        {
            var movies = new List<MovieDto>();

            var movieB17 = new MovieDto();
            movieB17.Title = "B";
            movieB17.MovieId = 17;
            movies.Add(movieB17);

            var movieB15 = new MovieDto();
            movieB15.Title = "B";
            movieB15.MovieId = 15;
            movies.Add(movieB15);

            var movieAb = new MovieDto();
            movieAb.Title = "AB";
            movieAb.MovieId = 10;
            movies.Add(movieAb);

            var movieAa2 = new MovieDto();
            movieAa2.Title = "AA";
            movieAa2.MovieId = 2;
            movies.Add(movieAa2);

            var movieAa1 = new MovieDto();
            movieAa1.Title = "AA";
            movieAa1.MovieId = 1;
            movies.Add(movieAa1);

            var movieAa3 = new MovieDto();
            movieAa3.Title = "AA";
            movieAa3.MovieId = 3;
            movies.Add(movieAa3);


            var sorter = new SortingService(new SortingStrategyFactory());

            movies = sorter.Sort("-title,movieid", movies);

            Assert.IsTrue(movies.First() == movieB15);
            Assert.IsTrue(movies.Last() == movieAa3);
        }
    }
}