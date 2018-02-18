using System.Collections.Generic;
using Smile.Movies.Dto;

namespace Smile.Movies.Backend.Interfaces
{
    /// <summary>
    ///     ISortingService represents signatures for particular SortingService instance class
    /// </summary>
    public interface ISortingService
    {
        /// <summary>
        ///     Sorts the specified sorting query.
        /// </summary>
        /// <param name="sortingQuery">The sorting query.</param>
        /// <param name="source">The source.</param>
        /// <returns>List of sorted MovieDto</returns>
        List<MovieDto> Sort(string sortingQuery, List<MovieDto> source);
    }
}