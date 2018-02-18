using System;
using Smile.Movies.Backend.Interfaces;
using Smile.Movies.Dto.Enums;

namespace Smile.Movies.Backend.SortingStrategies
{
    /// <summary>
    ///     SortingStrategyFactory creates particular sorting strategies by sortItem parameter
    /// </summary>
    /// <seealso cref="Smile.Movies.Backend.Interfaces.ISortingStrategyFactory" />
    public class SortingStrategyFactory : ISortingStrategyFactory
    {
        /// <summary>
        ///     Gets the specified sorting strategy instance.
        /// </summary>
        /// <param name="sortItem">The sort item.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public ISortingStrategy Get(SortingItems sortItem)
        {
            switch (sortItem)
            {
                case SortingItems.Classification:
                    return new ClassificationSortingStrategy();
                case SortingItems.Genre:
                    return new GenreSortingStrategy();
                case SortingItems.Rating:
                    return new RatingSortingStrategy();
                case SortingItems.ReleaseDate:
                    return new ReleaseDateSortingStrategy();
                case SortingItems.Title:
                    return new TitleSortingStrategy();
                case SortingItems.MovieId:
                    return new MovieIdSortingStrategy();
                default:
                    throw new ArgumentException($"A sorting strategy is not implemented for {sortItem.ToString()}.");
            }
        }
    }
}