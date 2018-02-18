namespace Smile.Movies.Shared
{
    /// <summary>
    ///     MemoryCacheSettings
    /// </summary>
    public class MemoryCacheSettings
    {
        /// <summary>
        ///     Gets or sets the expiration time hours.
        /// </summary>
        /// <value>
        ///     The expiration time hours.  
        /// </value>
        /// <remarks>
        ///     The Default value is 24 hours.
        /// </remarks>
        public int ExpirationTimeHours { get; set; } = 24;
    }
}