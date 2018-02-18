using System;
using System.Collections.Generic;
using System.Linq;
using Smile.Movies.Backend.Interfaces;
using Smile.Movies.Dto;
using Smile.Movies.Dto.Enums;
using Smile.Movies.Shared;

namespace Smile.Movies.Backend
{
    /// <summary>
    ///     SortService represents sorting strategies and allows to sort by multi fields
    /// </summary>
    /// <seealso cref="ISortingService" />
    public class SortingService : ISortingService
    {
        private readonly ISortingStrategyFactory _sortFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SortingService" /> class.
        /// </summary>
        /// <param name="sortFactory">The sort factory.</param>
        public SortingService(ISortingStrategyFactory sortFactory)
        {
            _sortFactory = sortFactory;
        }

        /// <summary>
        ///     Sorts the specified sorting query.
        /// </summary>
        /// <param name="sortingQuery">The sorting query is string with field names separated by ','</param>
        /// <param name="source">The source is list of MovieDto.</param>
        /// <returns>List of MovieDto</returns>
        public List<MovieDto> Sort(string sortingQuery, List<MovieDto> source)
        {
            Guard.NotNullOrEmpty(sortingQuery, nameof(sortingQuery));
            Guard.NotNull(source, nameof(source));

            if (string.IsNullOrWhiteSpace(sortingQuery) || !source.Any())
            {
                return source;
            }

            //sortingQuery includes names of fields separated by ','
            // '-' before field means that sorting order is descending otherwise ascending
            IOrderedEnumerable<MovieDto> orderedMovies = null;
            var isFirst = true;

            foreach (var str in sortingQuery.Split(","))
            {
                var orderBy = str;
                var sortDirrection = SortingDirections.Ascending;

                if (orderBy.StartsWith("-"))
                {
                    sortDirrection = SortingDirections.Descending;
                    orderBy = orderBy.Remove(0, 1);
                }

                if (Enum.TryParse(orderBy, true, out SortingItems sortBy))
                {
                    var sortStrategy = _sortFactory.Get(sortBy);

                    if (isFirst)
                    {
                        isFirst = false;
                        orderedMovies = sortStrategy.OrderBy(sortDirrection, source);
                    }
                    else
                    {
                        orderedMovies = sortStrategy.ThenBy(sortDirrection, orderedMovies);
                    }
                }
            }

            return orderedMovies?.ToList() ?? source;
        }
    }
}