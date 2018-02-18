using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Smile.Movies.Backend.Interfaces;
using Smile.Movies.Dto;

namespace Smile.Movies.Backend.Tests
{
    [TestClass]
    public class DataServiceTests
    {
        private ICachingService _cache;

        private IDataService _dataService;

        private IDataSourceAdapter _dataSource;

        private List<MovieDto> _movies;
        private ISearchingService _searcher;
        private ISortingService _sorter;

        [TestInitialize]
        public void TestInitialize()
        {
            _movies = new List<MovieDto>();

            var movie = new MovieDto();
            movie.Title = "Tom";
            movie.Cast = new List<string> {"Bird", "Penguin"};

            _movies.Add(movie);

            movie = new MovieDto();
            movie.Title = "Tom Tom";
            movie.Cast = new List<string> {"Abstraction", "NonConcreation"};

            _movies.Add(movie);

            _dataSource = Mock.Of<IDataSourceAdapter>();
            _searcher = Mock.Of<ISearchingService>();
            _sorter = Mock.Of<ISortingService>();
            _cache = Mock.Of<ICachingService>();

            _dataService = new DataService(_dataSource, _sorter, _searcher, _cache);
        }

        [TestMethod]
        public void GetAllMovies_PerformsSearchAndSort()
        {
            Mock.Get(_searcher).Setup(s => s.Search("Tom", _movies)).Returns(_movies);
            Mock.Get(_dataSource).Setup(d => d.GetAllMovies()).Returns(_movies);


            _dataService.GetAllMovies("Tom", "title");

            Mock.Get(_sorter).Verify(s => s.Sort("title", It.IsAny<List<MovieDto>>()), Times.Once);
            Mock.Get(_searcher).Verify(s => s.Search("Tom", It.IsAny<List<MovieDto>>()), Times.Once);
        }

        [TestMethod]
        public void GetAllMoviesWithNullQueryAndSortByParameters_NotPerformsSearchAndSort()
        {
            Mock.Get(_dataSource).Setup(d => d.GetAllMovies()).Returns(_movies);

            _dataService.GetAllMovies(string.Empty, string.Empty);

            Mock.Get(_sorter).Verify(s => s.Sort(It.IsAny<string>(), It.IsAny<List<MovieDto>>()), Times.Never);
            Mock.Get(_searcher).Verify(s => s.Search(It.IsAny<string>(), It.IsAny<List<MovieDto>>()), Times.Never);
        }

        [TestMethod]
        public void GetAllMovies_PerformsDataSourceGetAllData()
        {
            Mock.Get(_dataSource).Setup(d => d.GetAllMovies()).Returns(_movies);

            _dataService.GetAllMovies(string.Empty, string.Empty);

            Mock.Get(_dataSource).Verify(d => d.GetAllMovies());
        }

        [TestMethod]
        public void UpdateMovie_PerformsDataSourceUpdateMovie()
        {
            _dataService.UpdateMovie(_movies.First());

            Mock.Get(_dataSource).Verify(d => d.UpdateMovie(_movies.First()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateMovieWithNullParameter_ThrowsArgumentNullException()
        {
            _dataService.UpdateMovie(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetMovieByIdWithNegativeParameter_ThrowsArgumentOutOfRangeException()
        {
            _dataService.GetMovieById(-3);
        }

        [TestMethod]
        public void GetMovieById_PerformsDataSourceGetDataById()
        {
            _dataService.GetMovieById(1);

            Mock.Get(_dataSource).Verify(d => d.GetMovieById(1));
        }
    }
}