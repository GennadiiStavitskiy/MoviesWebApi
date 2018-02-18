using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Smile.Movies.Api.Controllers;
using Smile.Movies.Backend.Interfaces;
using Smile.Movies.Dto;

namespace Smile.Movies.Api.Tests
{
    [TestClass]
    public class MoviesControllerTests
    {
        private List<MovieDto> _allMovies;
        private IDataService _dataService;
        private ILoggerFactory _loggerFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            _loggerFactory = Mock.Of<ILoggerFactory>();
            Mock.Get(_loggerFactory).Setup(lf => lf.CreateLogger(It.IsAny<string>())).Returns(Mock.Of<ILogger>());

            _dataService = Mock.Of<IDataService>();

            _allMovies = new List<MovieDto>();

            var movie = new MovieDto();
            movie.MovieId = 2;
            movie.Title = "B";
            movie.Cast = new List<string> {"Bird", "Penguin"};

            _allMovies.Add(movie);

            movie = new MovieDto();
            movie.MovieId = 1;
            movie.Title = "A";
            movie.Cast = new List<string> {"Abstraction", "NonConcreation"};

            _allMovies.Add(movie);
        }

        [TestMethod]
        public void MoviesController_CanCreate()
        {
            var controller = new MoviesController(_dataService, _loggerFactory);

            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void GetAllMovies_ReturnNotFoundResult()
        {
            Mock.Get(_dataService).Setup(d => d.GetAllMovies(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<MovieDto>());

            var controller = new MoviesController(_dataService, _loggerFactory);

            var result = controller.GetAllMovies(string.Empty, string.Empty);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetAllMovies_ReturnOkResult()
        {
            Mock.Get(_dataService).Setup(d => d.GetAllMovies(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_allMovies);

            var controller = new MoviesController(_dataService, _loggerFactory);

            var result = controller.GetAllMovies(null, null);

            var contentResult = result as OkObjectResult;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Value);

            var resultMovies = contentResult.Value as List<MovieDto>;
            Assert.IsTrue(resultMovies.Count == _allMovies.Count);
        }

        [TestMethod]
        public void GetAllMovies_ReturnInternalServerError_WhenDataServiceThrowsException()
        {
            Mock.Get(_dataService).Setup(d => d.GetAllMovies(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());

            var controller = new MoviesController(_dataService, _loggerFactory);

            var result = controller.GetAllMovies(null, null);

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));

            var statusCode = result as StatusCodeResult;

            Assert.IsTrue(statusCode.StatusCode == StatusCodes.Status500InternalServerError);
        }

        [TestMethod]
        public void GetMovie_ReturnNotFoundResult()
        {
            var controller = new MoviesController(_dataService, _loggerFactory);

            var result = controller.GetMovie(1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetMovie_ReturnOkResult()
        {
            Mock.Get(_dataService).Setup(d => d.GetMovieById(It.IsAny<int>()))
                .Returns(_allMovies.First(m => m.MovieId == 1));

            var controller = new MoviesController(_dataService, _loggerFactory);

            var result = controller.GetMovie(1);

            var contentResult = result as OkObjectResult;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Value);

            var resultMovie = contentResult.Value as MovieDto;

            Assert.IsNotNull(resultMovie);
            Assert.IsTrue(resultMovie.MovieId == 1);
        }

        [TestMethod]
        public void GetMovie_ReturnInternalServerError_WhenDataServiceThrowsException()
        {
            Mock.Get(_dataService).Setup(d => d.GetMovieById(It.IsAny<int>()))
                .Throws(new Exception());

            var controller = new MoviesController(_dataService, _loggerFactory);

            var result = controller.GetMovie(1);

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));

            var statusCode = result as StatusCodeResult;

            Assert.IsTrue(statusCode.StatusCode == StatusCodes.Status500InternalServerError);
        }

        [TestMethod]
        public void CreateMovie_ReturnNotFoundResult()
        {
            var controller = new MoviesController(_dataService, _loggerFactory);

            var result = controller.CreateMovie(_allMovies.First());

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void CreateMovie_ReturnOkResult()
        {
            Mock.Get(_dataService).Setup(d => d.CreateMovie(It.IsAny<MovieDto>()))
                .Returns(_allMovies.First(m => m.MovieId == 1));

            var controller = new MoviesController(_dataService, _loggerFactory);

            var result = controller.CreateMovie(_allMovies.First(m => m.MovieId == 1));

            var contentResult = result as CreatedAtActionResult;

            Assert.IsTrue(contentResult.StatusCode == StatusCodes.Status201Created);

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Value);

            var resultMovie = contentResult.Value as MovieDto;

            Assert.IsNotNull(resultMovie);
        }

        [TestMethod]
        public void CreateMovie_ReturnBadRequestObjectResult()
        {
            var controller = new MoviesController(_dataService, _loggerFactory);

            controller.ModelState.Clear();

            controller.ModelState.AddModelError("Title", "The Title field is required.");

            var invalidMovie = new MovieDto();
            invalidMovie.MovieId = 1;

            var result = controller.CreateMovie(invalidMovie);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void CreateMovie_ReturnInternalServerError_WhenDataServiceThrowsException()
        {
            Mock.Get(_dataService).Setup(d => d.CreateMovie(It.IsAny<MovieDto>()))
                .Throws(new Exception());

            var controller = new MoviesController(_dataService, _loggerFactory);

            var result = controller.CreateMovie(_allMovies.First());

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));

            var statusCode = result as StatusCodeResult;

            Assert.IsTrue(statusCode.StatusCode == StatusCodes.Status500InternalServerError);
        }

        [TestMethod]
        public void UpdateMovie_ReturnBadRequestObjectResult()
        {
            var controller = new MoviesController(_dataService, _loggerFactory);

            controller.ModelState.Clear();

            controller.ModelState.AddModelError("Title", "The Title field is required.");

            var invalidMovie = new MovieDto();
            invalidMovie.MovieId = 1;

            var result = controller.UpdateMovie(invalidMovie);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void UpdateMovie_ReturnOkResult()
        {
            Mock.Get(_dataService).Setup(d => d.UpdateMovie(It.IsAny<MovieDto>()));

            var controller = new MoviesController(_dataService, _loggerFactory);

            var result = controller.UpdateMovie(_allMovies.First(m => m.MovieId == 1));

            var contentResult = result as OkObjectResult;

            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public void UpdateMovie_ReturnInternalServerError_WhenDataServiceThrowsException()
        {
            Mock.Get(_dataService).Setup(d => d.UpdateMovie(It.IsAny<MovieDto>()))
                .Throws(new Exception());

            var controller = new MoviesController(_dataService, _loggerFactory);

            var result = controller.UpdateMovie(_allMovies.First());

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));

            var statusCode = result as StatusCodeResult;

            Assert.IsTrue(statusCode.StatusCode == StatusCodes.Status500InternalServerError);
        }
    }
}