using System;
using CreatingAMovieAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreatingAMovieAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MovieController
	{
		public List<Movie> Movies = new List<Movie>
		{
			new Movie(){ Title="Forest Gump", Genre = Genre.Comedy, ReleaseYear=1993},
			new Movie(){ Title="Shrek", Genre = Genre.Comedy, ReleaseYear=2001},
			new Movie(){ Title="The Thing", Genre = Genre.Horror, ReleaseYear=1985},
			new Movie(){ Title="Finding Nemo", Genre = Genre.Animated, ReleaseYear=2003},
			new Movie(){ Title="The Notebook", Genre = Genre.Romance, ReleaseYear= 2005},
			new Movie(){ Title="Predator", Genre = Genre.Action, ReleaseYear=1985},
			new Movie(){ Title="Young Frankenstein", Genre = Genre.Comedy, ReleaseYear=1975}
		};

		[HttpGet("FullMovieList")]
		public List<Movie> GetMovies()
		{
			return Movies;
		}

		[HttpGet("SearchByGenre/{genre}")]
		public List<Movie> SearchByGenre(Genre genre)
		{
			List<Movie> result = Movies.Where(x => x.Genre == genre).ToList();
			return result;
		}

		[HttpGet("FindRandomMovie")]
		public Movie FindRandomMovie()
		{
			Random r = new Random();

			return Movies[r.Next(0, Movies.Count)];
		}

		[HttpGet("RandomMovieByGenre/{genre}")]
		public Movie RandomMovieByGenre(Genre genre)
		{
			Random r = new Random();
			List<Movie> result = Movies.Where(x => x.Genre == genre).ToList();

			return result[r.Next(0, result.Count)];
		}

		[HttpGet("RandomListOfMoviesByInt/{number}")]
		public List<Movie> RandomListOfMoviesByInt(int number)
        {
			Random r = new Random();
			List<Movie> result = new List<Movie>();
        
			for (int i =0; i <number; i++)
            {
				result.Add(Movies[r.Next(0, Movies.Count)]);
            }
			return result;
        }

		[HttpGet("FindMovieByKeyword/{searchterm}")]
		public Movie FindMovieByKeyword(string searchterm)
        {
			Movie result = (Movie)Movies.Where(x => x.Title.ToLower() == searchterm.ToLower());
			return result;
        }

		[HttpGet("SearchByKeyWord/{searchterm}")]
		public List<Movie> SearchByKeyWord(string searchterm)
        {
			List<Movie> result = Movies.Where(x => x.Title.ToLower().Contains(searchterm.ToLower())).ToList();
			return result;
        }

		[HttpGet("ShowCategories")]
		public List<Genre> ShowCategories()
        {
			List<Genre> categories = new List<Genre>();

			foreach (Genre g in Enum.GetValues(typeof(Genre)))
            {
				categories.Add(g);
            }

			return categories;
        }
	}
}

