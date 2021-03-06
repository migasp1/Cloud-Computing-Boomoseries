using boomoseries_IMDB_api.Data;
using boomoseries_IMDB_api.DTOs;
using boomoseries_IMDB_api.Mapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        //[HttpGet("movies")]
        //public async Task<IActionResult> GetMovies(double? min_rating)
        //{
        //    var movies = dataContext.Watchables.Where(watchable => watchable.Type == "Movie");
        //    List<WatchableDTO> movieDTOs = new();

        //    foreach (var movie in movies)
        //    {
        //        movieDTOs.Add(IMDBMapper.MapToDTO(movie));
        //    }

        //    if (min_rating == null)
        //    {
        //        return Ok(movieDTOs);

        //    }
        //    else if (min_rating != null)
        //    {
        //        var result = movieDTOs.Where(movie => movie.Rating >= min_rating);
        //        if (!result.Any())
        //        {
        //            return NotFound("There are no movies with more then rating " + min_rating + ".");
        //        }
        //        else
        //        {
        //            return Ok(result);
        //        }
        //    }

        //    return BadRequest("Something went wrong");
        //}

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
                WatchableDTO movieDTO = IMDBMapper.MapToDTO(movie);
                return Ok(movieDTO);
            }
        }

        //[HttpGet("series")]
        //public async Task<IActionResult> GetSeries(double? min_rating)
        //{
        //    var series = dataContext.Watchables.Where(watchable => watchable.Type == "TV Show");
        //    List<WatchableDTO> seriesDTOs = new();

        //    foreach (var serie in series)
        //    {
        //        seriesDTOs.Add(IMDBMapper.MapToDTO(serie));
        //    }

        //    if (min_rating == null)
        //    {
        //        return Ok(seriesDTOs);

        //    }
        //    else if (min_rating != null)
        //    {
        //        var result = seriesDTOs.Where(serie => serie.Rating >= min_rating);
        //        if (!result.Any())
        //        {
        //            return NotFound("There are no series with more then rating " + min_rating + ".");
        //        }
        //        else
        //        {
        //            return Ok(result);
        //        }
        //    }

        //    return BadRequest("Something went wrong");
        //}

        [HttpGet("series/{serie_title}")]
        public async Task<IActionResult> GetSpecificSerie(string serie_title)
        {
            var series = dataContext.Watchables.Where(watchable => watchable.Type == "TV Show");
            var serie = series.FirstOrDefault(serie => serie.Title.ToLower().Contains(serie_title.ToLower()));

            if (serie is null)
            {
                return NotFound("This serie doesn't exist");
            }
            else
            {
                WatchableDTO serieDTO = IMDBMapper.MapToDTO(serie);
                return Ok(serieDTO);
            }
        }
    }
}

