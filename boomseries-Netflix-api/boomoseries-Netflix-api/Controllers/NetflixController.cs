using boomoseries_Netflix_api.Data;
using boomoseries_Netflix_api.DTOs;
using boomoseries_Netflix_api.Mapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boomoseries_Netflix_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NetflixController : Controller
    {
        private readonly DataContext dataContext;

        public NetflixController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet("movies/random")]
        public async Task<IActionResult> GetRandomMovie()
        {
            var dbSet = dataContext.Watchables;

            var totalMovies = dbSet.Count();

            Random random = new ();

            int randomId = random.Next(1, totalMovies);

            var randomMovie = dbSet.Where(s => s.Type == "Movie").SingleOrDefault(s => s.Id == randomId);
            
            if (randomMovie == null)
            {
                return NotFound("This movie doesn't exist");
            }
            else
            {
                WatchableDTO movieDTO = NetflixMapper.MapToDTO(randomMovie);
                return Ok(movieDTO);
            }
        }

        [HttpGet("movies")]
        public async Task<IActionResult> GetMovies(double? min_rating)
        {
            var movies = dataContext.Watchables.Where(watchable => watchable.Type == "Movie");
            List<WatchableDTO> movieDTOs = new();

            foreach (var movie in movies)
            {
                movieDTOs.Add(NetflixMapper.MapToDTO(movie));
            }
                
            if (min_rating == null)
            {
                return Ok(movieDTOs);

            } else if (min_rating != null) 
            {
                var result = movieDTOs.Where(movie => movie.Rating >= min_rating);
                
                if (!result.Any())
                {
                    return NotFound("There are no movies with more then rating " + min_rating + ".");
                }
                else
                {
                    return Ok(result);
                }
            }

            return BadRequest("Something went wrong");
        }

        [HttpGet("movies/{movie_title}")]
        public async Task<IActionResult> GetSpecificMovie(string movie_title)
        {
            var movies = dataContext.Watchables.Where(watchable => watchable.Type == "Movie");
            var movie = movies.FirstOrDefault(movie => movie.Title.ToLower().Contains(movie_title.ToLower()));

            if (movie == null) 
            {
                return NotFound("This movie doesn't exist");
            }
            else
            {
                WatchableDTO movieDTO = NetflixMapper.MapToDTO(movie);
                return Ok(movieDTO);
            }
        }

        [HttpGet("series")]
        public async Task<IActionResult> GetSeries(double? min_rating)
        {
            var series = dataContext.Watchables.Where(watchable => watchable.Type == "TV Show");
            List<WatchableDTO> seriesDTOs = new();

            foreach (var serie in series)
            {
                seriesDTOs.Add(NetflixMapper.MapToDTO(serie));
            }

            if (min_rating == null)
            {
                return Ok(seriesDTOs);

            }
            else if (min_rating != null)
            {
                var result = seriesDTOs.Where(serie => serie.Rating >= min_rating);
                if (!result.Any())
                {
                    return NotFound("There are no series with more then rating " + min_rating + ".");
                }
                else
                {
                    return Ok(result);
                }
            }

            return BadRequest("Something went wrong");
        }

        [HttpGet("series/random")]
        public async Task<IActionResult> GetRandomSerie()
        {
            var dbSet = dataContext.Watchables;

            var totalSeries = dbSet.Count();

            Random random = new();

            int randomId = random.Next(1, totalSeries);

            var randomSerie = dbSet.Where(s => s.Type == "TV Show").SingleOrDefault(s => s.Id == randomId);

            if (randomSerie == null)
            {
                return NotFound("This serie doesn't exist");
            }
            else
            {
                WatchableDTO serieDTO = NetflixMapper.MapToDTO(randomSerie);
                return Ok(serieDTO);
            }
        }

        [HttpGet("series/{serie_title}")]
        public async Task<IActionResult> GetSpecificSerie(string serie_title)
        {
            var series = dataContext.Watchables.Where(watchable => watchable.Type == "TV Show");
            var serie = series.FirstOrDefault(serie => serie.Title.ToLower().Contains(serie_title.ToLower()));

            if (serie == null)
            {
                return NotFound("This serie doesn't exist");
            }
            else
            {
                WatchableDTO serieDTO = NetflixMapper.MapToDTO(serie);
                return Ok(serieDTO);
            }
        }
    }
}
