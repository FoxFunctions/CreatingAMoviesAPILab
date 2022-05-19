using System;
using CreatingAMovieAPI.models;
using CreatingAMovieAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreatingAMovieAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MovieController
	{
		MoviesDbContext context = new MoviesDbContext();
		public List<Movie> Movies = new List<Movie>
		
		{
			/*new Movie(){ Title="Forest Gump", Genre = Genre.Comedy, ReleaseYear=1993},
			new Movie(){ Title="Shrek", Genre = Genre.Comedy, ReleaseYear=2001},
			new Movie(){ Title="The Thing", Genre = Genre.Horror, ReleaseYear=1985},
			new Movie(){ Title="Finding Nemo", Genre = Genre.Animated, ReleaseYear=2003},
			new Movie(){ Title="The Notebook", Genre = Genre.Romance, ReleaseYear= 2005},
			new Movie(){ Title="Predator", Genre = Genre.Action, ReleaseYear=1985},
			new Movie(){ Title="Young Frankenstein", Genre = Genre.Comedy, ReleaseYear=1975},
			new Movie(){ Title="Zoolander", Genre = Genre.Comedy, ReleaseYear=2001}*/

		};

		[HttpGet("FullMovieList")]
		public List<Movie> GetMovies()
		{
			return context.Movies.ToList();
		}

		[HttpGet("SearchByGenre/{genres}")]
		public List<Movie> SearchByGenre(int genres)
		{
			List<Movie> result = context.Movies.Where(x => x.Genre == genres).ToList();
			return result;
		}

		[HttpGet("FindRandomMovie")]
		public Movie FindRandomMovie()
		{
			Random r = new Random();

			return Movies[r.Next(0, Movies.Count)];
		}

		[HttpGet("RandomMovieByGenre/{genre}")]
		public Movie RandomMovieByGenre(int genre)
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
        
			for (int i = 0; i <number; i++)
            {
				result.Add(Movies[r.Next(0, Movies.Count)]);
            }
			return result;
        }

		[HttpGet("FindMovieByKeyword/{searchterm}")]
		public Movie FindMovieByKeyword(string searchterm)
        {
			for (int i = 0; i < Movies.Count; i++)
            {
				if (Movies[i].Title.ToLower().Contains(searchterm.ToLower()))
				{
					return Movies[i];
				}
            }
			return null;
        }		

		[HttpGet("ReturnListByKeyWord/{searchterm}")]
		public List<Movie> ReturnListByKeyWord(string searchterm)
        {
			List<Movie> result = Movies.Where(x => x.Title.ToLower().Contains(searchterm.ToLower())).ToList();
			return result;
        }

		[HttpGet("ShowCategories")]
		public List<string> ShowCategories()
        {
			List<string> categories = new List<string>();

			foreach (string g in Enum.GetValues(typeof(string)))
            {
				categories.Add(g);
            }
			return categories;
        }

		[HttpGet("GetMovie/{id}")]
		public Movie GetMovie(int id)
		{
			try
			{
				Movie output = context.Movies.Find(id);
				return output;
			}
			catch (Exception ex)
			{
				Movie m = new Movie();
				m.Title = ex.Message;
				return m;
			}
		}

		[HttpGet("RandomListOfMoviesByGenreRandomCount/{genre}&{number}")]
		public List<Movie> GetRandomMovieListByGenreRandomCount(int genre, int number)
        {
			List<Movie> byGenre = Movies.Where(x => x.Genre == genre).ToList();
			List<Movie> result = new List<Movie>();
			Random r = new Random();
			
			for (int i = 0; i < number; i++)
            {   int num = r.Next(0, byGenre.Count);
				result.Add(byGenre[num]);
				byGenre.Remove(byGenre[num]);
			}
			return result;
		}

		[HttpPost("Create")]
		public void CreateMovie(Movie input)
        {
			context.Movies.Add(input);
			context.SaveChanges();
        }

		[HttpDelete("Delete/{id}")]
		public string DeleteMovie(int id)
        {
			int initialCount = context.Movies.Count();
			try
			{
				context.Movies.Remove(GetMovie(id));
			}
            catch (Exception e)
            {
				string errorOutput = e.Message;
				errorOutput += "\n No Changes made to the database";
				return errorOutput;
            }
			context.SaveChanges();
			int finalCount = context.Movies.Count();

			return $"{initialCount} and {finalCount}";
        }

		[HttpPut("Update/{id}")]
		public string UpdateMovie(int id, Movie updatedMovie)
        {
			Movie movie = GetMovie(id);
			updatedMovie.Id = movie.Id;
			movie.Title = updatedMovie.Title;
			movie.ReleaseYear = updatedMovie.ReleaseYear;
			movie.Genre = updatedMovie.Genre;
			context.Movies.Update(movie);
			context.SaveChanges();
			return $"{updatedMovie.Title} at ID: {updatedMovie.Id} has been updated";
        }
	}
}

