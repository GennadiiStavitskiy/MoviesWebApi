using Smile.Movies.Dto.Enums;

namespace Smile.Movies.Backend.Interfaces
{
    /// <summary>
    ///     ISortingStrategyFactory represents signatures for particular SortingStrategyFactory instance class
    /// </summary>
    public interface ISortingStrategyFactory
    {
        /// <summary>
        ///     Gets the specified sorting strategy instance.
        /// </summary>
        /// <param name="sortItem">The sort item.</param>
        /// <returns>ISortingStrategy</returns>
        ISortingStrategy Get(SortingItems sortItem);
    }
}