using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Smile.Movies.Backend.Interfaces;
using Smile.Movies.Dto;
using Smile.Movies.Shared;

namespace Smile.Movies.Api.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private readonly IDataService _dataService;
        private readonly ILogger _logger;

        public MoviesController(IDataService dataService, ILoggerFactory factory)
        {
            _dataService = dataService;
            _logger = factory.CreateLogger("Smile.Movies.Api");
        }

        /// <summary>
        ///     Gets all movies.
        /// </summary>
        /// <remarks>
        ///     GET api/movies
        /// </remarks>
        /// <param name="search">The search.</param>
        /// <param name="sort">The sort.</param>
        /// <returns>A list of Movies</returns>
        /// <response code="200">Returns if at least one movie was found</response>
        /// <response code="404">If the list is null or empty</response>
        /// <response code="500">If exception is thrown during fetching movies</response>
        [HttpGet("")]
        public IActionResult GetAllMovies(string search, string sort)
        {
            try
            {
                var movies = _dataService.GetAllMovies(search, sort);

                if (movies == null || !movies.Any())
                {
                    return NotFound();
                }

                return Ok(movies);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "MoviesController.GetAllMovies exception");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        ///     Gets a movie by movie id.
        /// </summary>
        /// <remarks>
        ///     GET api/movies/5
        /// </remarks>
        /// <param name="id">MovieId</param>
        /// <returns>A MovieDto</returns>
        /// <response code="200">Returns if the movie was found</response>
        /// <response code="404">If movie is null</response>
        /// <response code="500">If exception is thrown during fetching movies</response>
        [HttpGet("{id}")]
        public IActionResult GetMovie(int id)
        {
            try
            {
                var movie = _dataService.GetMovieById(id);

                if (movie == null)
                {
                    return NotFound();
                }

                return Ok(movie);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "MoviesController.GetMovie exception");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        ///     Create new movie.
        /// </summary>
        /// <remarks>
        ///     POST api/movies
        ///     {
        ///     "classification": "string",
        ///     "genre": "string",
        ///     "movieId": 0,
        ///     "rating": 0,
        ///     "releaseDate": 0,
        ///     "title": "string",
        ///     "cast": [ "string"]
        ///     }
        /// </remarks>
        /// <param name="movie">MovieDto</param>
        /// <returns>A MovieDto</returns>
        /// <response code="201">Returns if the movie is created</response>
        /// <response code="400">If movie is not valid</response>
        /// <response code="500">If exception is thrown during creating movie</response>
        [HttpPost("")]
        public IActionResult CreateMovie([FromBody] MovieDto movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    movie = _dataService.CreateMovie(movie);

                    if (movie == null)
                    {
                        return NotFound();
                    }

                    return CreatedAtAction("CreateMovie", new {movie.MovieId}, movie);
                }

                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "MoviesController.CreateMovie exception");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        ///     Update the movie.
        /// </summary>
        /// <remarks>
        ///     PUT api/movies/
        ///     {
        ///     "classification": "string",
        ///     "genre": "string",
        ///     "movieId": 0,
        ///     "rating": 0,
        ///     "releaseDate": 0,
        ///     "title": "string",
        ///     "cast": [ "string"]
        ///     }
        /// </remarks>
        /// <param name="movie">MovieDto</param>
        /// <returns>A MovieDto</returns>
        /// <response code="200">Returns if the movie was updated</response>
        /// <response code="400">If movie is not valid</response>
        /// <response code="500">If exception is thrown during updating movie</response>
        [HttpPut("")]
        public IActionResult UpdateMovie([FromBody] MovieDto movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dataService.UpdateMovie(movie);

                    return Ok(movie);
                }

                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "MoviesController.UpdateMovie exception");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}