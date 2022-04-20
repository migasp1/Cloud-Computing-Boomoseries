using AmazonPrime_Microservice.Data;
using boomoseries_Amazon_api;
using boomoseries_Amazon_api.DTOs;
using boomoseries_Amazon_api.Mapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonPrime_Microservice.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AmazonController : Controller
    {
        private readonly DataContext dataContext;
        public AmazonController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet("movies/random")]
        public async Task<IActionResult> GetRandomMovie()
        {
            var dbSet = dataContext.Watchables;

            List<WatchableDTO> movies = new List<WatchableDTO>();
            var rows = dbSet.Where(s => s.Type == "Movie").OrderBy(t => Guid.NewGuid())
                        .Take(1);

            foreach (var item in rows)
            {
                WatchableDTO movieDTO = AmazonMapper.MapToDTO(item);
                movies.Add(movieDTO);
            }


            if (rows == null)
            {
                return NotFound("This movie doesn't exist");
            }
            else
            {
                return Ok(movies);
            }
        }

        [HttpGet("movies")]
        public async Task<IActionResult> GetMovies(double? min_rating)
        {
            var movies = dataContext.Watchables.Where(Watchable => Watchable.Type == "Movie").Take(2);
            List<WatchableDTO> movieDTOs = new();

            foreach (var movie in movies)
            {
                movieDTOs.Add(AmazonMapper.MapToDTO(movie));
            }

            if (min_rating == null)
            {
                return Ok(movieDTOs);

            }
            else if (min_rating != null)
            {
                var result = movieDTOs.Where(movie => movie.Rating >= min_rating);
                if (!result.Any())
                {
                    return NotFound("There are no movies with more than rating" + min_rating + ".");
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
            var movies = dataContext.Watchables.Where(Watchable => Watchable.Type == "Movie");
            var movie = movies.FirstOrDefault(movie => movie.Title.ToLower().Contains(movie_title.ToLower()));

            if (movie == null)
            {
                return NotFound("This movie doesn´t exist");
            }
            else
            {
                WatchableDTO movieDTO = AmazonMapper.MapToDTO(movie);
                return Ok(movieDTO);
            }
        }

        [HttpGet("series/random")]
        public async Task<IActionResult> GetRandomSerie()
        {
            var dbSet = dataContext.Watchables;

            List<WatchableDTO> series = new List<WatchableDTO>();
            var rows = dbSet.Where(s => s.Type == "TV Show").OrderBy(t => Guid.NewGuid())
                        .Take(1);

            foreach (var item in rows)
            {
                WatchableDTO serieDTO = AmazonMapper.MapToDTO(item);
                series.Add(serieDTO);
            }

            if (rows == null)
            {
                return NotFound("This serie doesn't exist");
            }
            else
            {
                return Ok(series);
            }
        }

        [HttpGet("series")]
        public async Task<IActionResult> GetSeries(double? min_rating)
        {
            var series = dataContext.Watchables.Where(Watchable => Watchable.Type == "TV Show").Take(2);
            List<WatchableDTO> seriesDTOs = new();

            foreach (var serie in series)
            {
                seriesDTOs.Add(AmazonMapper.MapToDTO(serie));
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

        [HttpGet("series/{serie_title}")]
        public async Task<IActionResult> GetSpecificSerie(string serie_title)
        {
            var serie = dataContext.Watchables.Where(Watchable => Watchable.Type == "TV Show").FirstOrDefault(serie => serie.Title.ToLower().Contains(serie_title.ToLower()));

            if (serie == null)
            {
                return NotFound("This serie doesn't exist");
            }
            else
            {
                WatchableDTO serieDTO = AmazonMapper.MapToDTO(serie);
                return Ok(serieDTO);
            }
        }
    }
}