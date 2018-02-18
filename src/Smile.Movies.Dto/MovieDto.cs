using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Smile.Movies.Dto
{
    /// <summary>
    ///     MovieDto
    /// </summary>
    public class MovieDto
    {
        /// <summary>
        ///     Gets or sets the classification.
        /// </summary>
        /// <value>
        ///     The classification.
        /// </value>
        [Required]
        public string Classification { get; set; }

        /// <summary>
        ///     Gets or sets the genre.
        /// </summary>
        /// <value>
        ///     The genre.
        /// </value>
        [Required]
        public string Genre { get; set; }

        /// <summary>
        ///     Gets or sets the movie identifier.
        /// </summary>
        /// <value>
        ///     The movie identifier.
        /// </value>
        public int MovieId { get; set; }

        /// <summary>
        ///     Gets or sets the rating.
        /// </summary>
        /// <value>
        ///     The rating.
        /// </value>
        public int Rating { get; set; }

        /// <summary>
        ///     Gets or sets the release date.
        /// </summary>
        /// <value>
        ///     The release date.
        /// </value>
        [Required]
        public int ReleaseDate { get; set; }

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        /// <value>
        ///     The title.
        /// </value>
        [Required]
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets the cast.
        /// </summary>
        /// <value>
        ///     The cast.
        /// </value>
        public List<string> Cast { get; set; }
    }
}