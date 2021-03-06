﻿using System.Collections.Generic;
using System.Linq;
using Smile.Movies.Backend.Interfaces;
using Smile.Movies.Dto;
using Smile.Movies.Dto.Enums;

namespace Smile.Movies.Backend.SortingStrategies
{
    /// <summary>
    ///     ReleaseDateSortingStrategy
    /// </summary>
    /// <seealso cref="Smile.Movies.Backend.Interfaces.ISortingStrategy" />
    public class ReleaseDateSortingStrategy : ISortingStrategy
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
                return movies.OrderBy(m => m.ReleaseDate);
            }

            return movies.OrderByDescending(m => m.ReleaseDate);
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
                return movies.ThenBy(m => m.ReleaseDate);
            }

            return movies.ThenByDescending(m => m.ReleaseDate);
        }
    }
}