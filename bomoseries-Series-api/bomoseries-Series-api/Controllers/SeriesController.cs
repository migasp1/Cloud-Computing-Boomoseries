using Microsoft.AspNetCore.Mvc;
using bomoseries_Series_api.DTOs;
using bomoseries_Series_api.Services;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace bomoseries_Series_api.Controllers
{
    public class SeriesController : Controller
    {
        private readonly ICommunicationService commService;
        public SeriesController(ICommunicationService commService)
        {
            this.commService = commService;
        }
        [HttpGet("/api/v1/Series/{series_title}")]
        public async Task<IActionResult> GetSpecificSeries(string series_title)
        {
            try
            {
                var responseBody = await commService.ObtainSepcificSeries(series_title);
                //MovieDTO deserializedMovie = JsonConvert.DeserializeObject<MovieDTO>(responseBody);
                return Ok(responseBody);
            }
            catch (Exception ex)
            {
                return BadRequest("Oops, something went wrong! " + ex.Message);
            }
        }

        [HttpGet("/api/v1/Series")]
        public async Task<IActionResult> GetSeriesByRating(double minRating)
        {
            //[FromQuery(Name = "min_rating")]
            try
            {
                var responseBody = await commService.GetSeriesByRating(minRating);
                return Ok(responseBody);
            }
            catch (Exception ex)
            {
                return BadRequest("Oops, something went wrong! " + ex.Message);
            }

        }

        [HttpGet("/api/v1/Series/random")]
        public async Task<IActionResult> GetRandomSeries()
        {
            try
            {
                var responseBody = await commService.ObtainRandomSeries();
                return Ok(responseBody);
            }
            catch (Exception ex)
            {
                return BadRequest("Oops, something went wrong! " + ex.Message);
            }

        }
    }
}