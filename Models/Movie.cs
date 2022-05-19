using System;
using System.Collections.Generic;

namespace CreatingAMovieAPI.models
{
    public partial class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? ReleaseYear { get; set; }
        public int? Genre { get; set; }

        public virtual Genre? GenreNavigation { get; set; }
    }
}
