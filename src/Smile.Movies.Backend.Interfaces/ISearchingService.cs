using System.Collections.Generic;
using Smile.Movies.Dto;

namespace Smile.Movies.Backend.Interfaces
{
    /// <summary>
    ///     ISearchingService represents signatures for particular SearchingService instance class
    /// </summary>
    public interface ISearchingService
    {
        /// <summary>
        ///     Searches the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="movies">The movies.</param>
        /// <returns>List of found MovieDto</returns>
        List<MovieDto> Search(string query, List<MovieDto> movies);
    }
}