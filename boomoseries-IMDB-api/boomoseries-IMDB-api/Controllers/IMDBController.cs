using boomoseries_IMDB_api.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace boomoseries_IMDB_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IMDBController : Controller
    {
        private readonly DataContext dataContext;

        public IMDBController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet("movies")]
        public async Task<IActionResult> GetMovies(float? min_rating)
        {
            var movies = dataContext.Watchables.Where(watchable => watchable.Type == "Movie");

            if (min_rating == null)
            {
                return Ok(movies);

            }
            else if (min_rating != null)
            {
                var movie = movies.Where(movie => movie.Rating >= min_rating);
                if (!movie.Any())
                {
                    return NotFound("There are no movies with more then rating " + min_rating + ".");
                }
                else
                {
                    return Ok(movie);
                }
            }

            return BadRequest("Something went wrong");
        }

        [HttpGet("movies/{movie_title}")]
        public async Task<IActionResult> GetSpecificMovie(string movie_title)
        {
            var movies = dataContext.Watchables.Where(watchable => watchable.Type == "Movie");
            var movie = movies.FirstOrDefault(movies => movies.Title == movie_title);

            if (movie == null)
            {
                return NotFound("This movie doesn't exist");
            }
            else
            {
                return Ok(movie);
            }
        }

        [HttpGet("series")]
        public async Task<IActionResult> GetSeries(float? min_rating)
        {
            var series = dataContext.Watchables.Where(watchable => watchable.Type == "TV Show");

            if (min_rating == null)
            {
                return Ok(series);

            }
            else if (min_rating != null)
            {
                var serie = series.Where(serie => serie.Rating >= min_rating);
                if (!serie.Any())
                {
                    return NotFound("There are no series with more then rating " + min_rating + ".");
                }
                else
                {
                    return Ok(serie);
                }
            }

            return BadRequest("Something went wrong");
        }

        [HttpGet("series/{serie_title}")]
        public async Task<IActionResult> GetSpecificSerie(string serie_title)
        {
            var series = dataContext.Watchables.Where(watchable => watchable.Type == "TV Show");
            var serie = series.FirstOrDefault(series => series.Title == serie_title);

            if (serie is null)
            {
                return NotFound("This serie doesn't exist");
            }
            else
            {
                return Ok(serie);
            }
        }
    }
}

