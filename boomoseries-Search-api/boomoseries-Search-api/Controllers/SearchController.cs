using boomoseries_Search_api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace boomoseries_Search_api.Controllers
{
    public class SearchController : Controller
    {
        private readonly ICommunicationServiceMovies commServiceMovies;
        private readonly ICommunicationServiceSeries commServiceSeries;
        private readonly ICommunicationServiceBooks commServiceBooks;

        public SearchController(ICommunicationServiceMovies commServiceMovies, ICommunicationServiceSeries commServiceSeries, ICommunicationServiceBooks commServiceBooks)
        {
            this.commServiceMovies = commServiceMovies;
            this.commServiceSeries = commServiceSeries;
            this.commServiceBooks = commServiceBooks;
        }

        [HttpGet("/api/v1/Search")]
        public async Task<IActionResult> GetItemsByRating(string type, double minRating)
        {
            if (type.Equals("Movie")) {
                try
                {
                    var responseBody = await commServiceMovies.GetMoviesByRating(minRating);
                    return Ok(responseBody);
                }
                catch (Exception ex)
                {
                    return BadRequest("Oops, something went wrong! " + ex.Message);
                }
            }
            else if (type.Equals("Serie"))
            {
                try
                {
                    var responseBody = await commServiceSeries.GetSeriesByRating(minRating);
                    return Ok(responseBody);
                }
                catch (Exception ex)
                {
                    return BadRequest("Oops, something went wrong! " + ex.Message);
                }
            }
            else if (type.Equals("Book"))
            {
                try
                {
                    var responseBody = await commServiceBooks.ObtainBooksByRating(minRating);
                    return Ok(responseBody);
                }
                catch (Exception ex)
                {
                    return BadRequest("Oops, something went wrong! " + ex.Message);
                }
            }
            return BadRequest("Oops, something went wrong! ");
        }

        [HttpGet("/api/v1/Search/{item_title}")]
        public async Task<IActionResult> GetSpecificItem(string type, string item_title)
        {
            if (type.Equals("Movie"))
            {
                try
                {
                    var responseBody = await commServiceMovies.ObtainSepcificMovie(item_title);
                    return Ok(responseBody);
                }
                catch (Exception ex)
                {
                    return BadRequest("Oops, something went wrong! " + ex.Message);
                }
            }
            else if (type.Equals("Serie"))
            {
                try
                {
                    var responseBody = await commServiceSeries.ObtainSepcificSeries(item_title);
                    return Ok(responseBody);
                }
                catch (Exception ex)
                {
                    return BadRequest("Oops, something went wrong! " + ex.Message);
                }
            }
            else if (type.Equals("Book"))
            {
                try
                {
                    var responseBody = await commServiceBooks.ObtainSpecificBook(item_title);
                    return Ok(responseBody);
                }
                catch (Exception ex)
                {
                    return BadRequest("Oops, something went wrong! " + ex.Message);
                }
            }
            return BadRequest("Oops, something went wrong! ");
        }

        [HttpGet("/api/v1/Search/random")]
        public async Task<IActionResult> GetRandomItems(string type)
        {
            if (type.Equals("Movie"))
            {
                try
                {
                    var responseBody = await commServiceMovies.ObtainRandomMovies();
                    return Ok(responseBody);
                }
                catch (Exception ex)
                {
                    return BadRequest("Oops, something went wrong! " + ex.Message);
                }
            }
            else if (type.Equals("Serie"))
            {
                try
                {
                    var responseBody = await commServiceSeries.ObtainRandomSeries();
                    return Ok(responseBody);
                }
                catch (Exception ex)
                {
                    return BadRequest("Oops, something went wrong! " + ex.Message);
                }
            }
            else if (type.Equals("Book"))
            {
                try
                {
                    var responseBody = await commServiceBooks.ObtainRandomBooks();
                    return Ok(responseBody);
                }
                catch (Exception ex)
                {
                    return BadRequest("Oops, something went wrong! " + ex.Message);
                }
            }
            return BadRequest("Oops, something went wrong! ");
        }

    }
}
