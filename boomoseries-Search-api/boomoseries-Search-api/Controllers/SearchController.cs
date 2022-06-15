using boomoseries_Search_api.DTOs;
using boomoseries_Search_api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            if (type.Equals("Movie"))
            {
                var responseBody = await commServiceMovies.GetMoviesByRating(minRating);
                if (responseBody is List<MovieDTO>)
                {
                    List<MovieDTO> responseBodyTyped = (List<MovieDTO>)responseBody;
                    return Ok(responseBody);
                }
                return BadRequest((string)responseBody);

            }
            else if (type.Equals("TV Show"))
            {

                var responseBody = await commServiceSeries.GetSeriesByRating(minRating);
                if (responseBody is List<SerieDTO>)
                {
                    List<SerieDTO> responseBodyTyped = (List<SerieDTO>)responseBody;
                    return Ok(responseBodyTyped);
                }
                return BadRequest((string)responseBody);

            }
            else if (type.Equals("Book"))
            {

                var responseBody = await commServiceBooks.ObtainBooksByRating(minRating);
                if (responseBody is List<BookDTO>)
                {
                    List<BookDTO> responseBodyTyped = (List<BookDTO>)responseBody;
                    return Ok(responseBodyTyped);
                }
                return BadRequest((string)responseBody);

            }
            return BadRequest("Oops, something went wrong! ");
        }

        [HttpGet("/api/v1/Search/{item_title}")]
        public async Task<IActionResult> GetSpecificItem(string type, string item_title)
        {
            if (type.Equals("Movie"))
            {

                var responseBody = await commServiceMovies.ObtainSepcificMovie(item_title);
                if (responseBody is List<MovieDTO>)
                {
                    List<MovieDTO> responseBodyTyped = (List<MovieDTO>)responseBody;
                    return Ok(responseBody);
                }
                return BadRequest((string)responseBody);

            }
            else if (type.Equals("TV Show"))
            {

                var responseBody = await commServiceSeries.ObtainSepcificSeries(item_title);
                if (responseBody is List<SerieDTO>)
                {
                    List<SerieDTO> responseBodyTyped = (List<SerieDTO>)responseBody;
                    return Ok(responseBodyTyped);
                }
                return BadRequest((string)responseBody);

            }
            else if (type.Equals("Book"))
            {

                var responseBody = await commServiceBooks.ObtainSpecificBook(item_title);
                if (responseBody is List<BookDTO>)
                {
                    List<BookDTO> responseBodyTyped = (List<BookDTO>)responseBody;
                    return Ok(responseBody);
                }
                return BadRequest((string)responseBody);

            }
            return BadRequest("Oops, something went wrong! ");
        }

        [HttpGet("/api/v1/Search/random")]
        public async Task<IActionResult> GetRandomItems(string type)
        {
            if (type.Equals("Movie"))
            {

                var responseBody = await commServiceMovies.ObtainRandomMovies();
                if (responseBody is List<MovieDTO>)
                {
                    List<MovieDTO> responseBodyTyped = (List<MovieDTO>)responseBody;
                    return Ok(responseBody);
                }
                return BadRequest((string)responseBody);

            }
            else if (type.Equals("TV Show"))
            {

                var responseBody = await commServiceSeries.ObtainRandomSeries();
                if (responseBody is List<SerieDTO>)
                {
                    List<SerieDTO> responseBodyTyped = (List<SerieDTO>)responseBody;
                    return Ok(responseBody);
                }
                return BadRequest((string)responseBody);

            }
            else if (type.Equals("Book"))
            {

                var responseBody = await commServiceBooks.ObtainRandomBooks();
                if (responseBody is List<BookDTO>)
                {
                    List<BookDTO> responseBodyTyped = (List<BookDTO>)responseBody;
                    return Ok(responseBody);
                }
                return BadRequest((string)responseBody);

            }
            return BadRequest("Oops, something went wrong! ");
        }

    }
}
