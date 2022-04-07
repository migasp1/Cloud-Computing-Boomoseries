using boomoseries_Movies_api.DTOs;
using boomoseries_Movies_api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace boomoseries_Movies_api.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ICommunicationService commService;
        public MoviesController(ICommunicationService commService)
        {
            this.commService = commService;
        }

        [HttpGet("/api/v1/Movies/{movie_title}")]
        public async Task<IActionResult> GetSpecificMovie(string movie_title)
        {
            try
            {
                var responseBody = await commService.ObtainSepcificMovie(movie_title);
                //MovieDTO deserializedMovie = JsonConvert.DeserializeObject<MovieDTO>(responseBody);
                return Ok(responseBody);    
            }
            catch (Exception ex)
            {
               return BadRequest("Oops, something went wrong! " + ex.Message);
            }
        }

        /*
        [HttpGet("/api/v1/Movies/")]
        public async Task<IActionResult> GetMovies()
        {
            try
            {
                var responseBody = await commService.ObtainMovies();
                //MovieDTO deserializedMovie = JsonConvert.DeserializeObject<MovieDTO>(responseBody);
                return Ok(responseBody);
            }
            catch (Exception ex)
            {
                return BadRequest("Oops, something went wrong! " + ex.Message);
            }
        }
        */

        [HttpGet("/api/v1/Movies")]
        public async Task<IActionResult> GetMoviesByRating(double minRating)
        {
            //[FromQuery(Name = "min_rating")]
            try
            {
                var responseBody = await commService.GetMoviesByRating(minRating);
                return Ok(responseBody);
            }
            catch (Exception ex)
            {
                return BadRequest("Oops, something went wrong! " + ex.Message);
            }

        }
    }
}
