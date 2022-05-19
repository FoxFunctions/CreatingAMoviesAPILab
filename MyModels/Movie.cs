using System;
namespace CreatingAMovieAPI.Models
{	public enum Genre
    {
		Horror,
		Comedy,
		Action,
		Animated,
		Romance

    }
	public class OldMovie
	{
		public string Title { get; set; }
        public int ReleaseYear { get; set; }
		public Genre Genre { get; set; }
    }
}

