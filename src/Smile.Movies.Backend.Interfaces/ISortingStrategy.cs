using System.Collections.Generic;
using System.Linq;
using Smile.Movies.Dto;
using Smile.Movies.Dto.Enums;

namespace Smile.Movies.Backend.Interfaces
{
    /// <summary>
    ///     ISortingStrategy interface represents methods signatures for particular sorting strategies
    /// </summary>
    public interface ISortingStrategy
    {
        /// <summary>
        ///     Sort input movies with sorting direction provided by parameter.
        /// </summary>
        /// <param name="direction">Sorting direction: {Ascending, Descending}.</param>
        /// <param name="movies">The movies.</param>
        /// <returns>IOrderedEnumerable of MovieDto</returns>
        IOrderedEnumerable<MovieDto> OrderBy(SortingDirections direction, List<MovieDto> movies);

        /// <summary>
        ///     Sort input by ThenBy method the movies with sorting direction provided by parameter.
        /// </summary>
        /// <param name="dirrection">Sorting direction: {Ascending, Descending}.</param>
        /// <param name="movies">The movies.</param>
        /// <returns>IOrderedEnumerable of MovieDto</returns>
        IOrderedEnumerable<MovieDto> ThenBy(SortingDirections dirrection, IOrderedEnumerable<MovieDto> movies);
    }
}