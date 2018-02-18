using System.Collections.Generic;
using System.Linq;
using Smile.Movies.Backend.Interfaces;
using Smile.Movies.Dto;
using Smile.Movies.Dto.Enums;

namespace Smile.Movies.Backend.SortingStrategies
{
    /// <summary>
    ///     RatingSortingStrategy
    /// </summary>
    /// <seealso cref="Smile.Movies.Backend.Interfaces.ISortingStrategy" />
    public class RatingSortingStrategy : ISortingStrategy
    {
        /// <summary>
        ///     Sort input movies with sorting direction provided by parameter.
        /// </summary>
        /// <param name="direction">Sorting direction: {Ascending, Descending}.</param>
        /// <param name="movies">The movies.</param>
        /// <returns>IOrderedEnumerable of MovieDto</returns>
        public IOrderedEnumerable<MovieDto> OrderBy(SortingDirections direction, List<MovieDto> movies)
        {
            if (direction == SortingDirections.Ascending)
            {
                return movies.OrderBy(m => m.Rating);
            }

            return movies.OrderByDescending(m => m.Rating);
        }

        /// <summary>
        ///     Sort input by ThenBy method the movies with sorting direction provided by parameter.
        /// </summary>
        /// <param name="dirrection">Sorting direction: {Ascending, Descending}.</param>
        /// <param name="movies">The movies.</param>
        /// <returns>
        ///     IOrderedEnumerable of MovieDto
        /// </returns>
        public IOrderedEnumerable<MovieDto> ThenBy(SortingDirections dirrection, IOrderedEnumerable<MovieDto> movies)
        {
            if (dirrection == SortingDirections.Ascending)
            {
                return movies.ThenBy(m => m.Rating);
            }

            return movies.ThenByDescending(m => m.Rating);
        }
    }
}