using boomoseries_Books_api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace boomoseries_Books_api.Controllers
{
    public class BooksController : Controller
    {
        private readonly ICommunicationService commService;

        public BooksController(ICommunicationService commService)
        {
            this.commService = commService;
        }

        [HttpGet("/api/v1/books/{book_title}")]
        public async Task<IActionResult> GetSpecificMovie(string book_title)
        {
            try
            {
                var responseBody = await commService.ObtainSpecificBook(book_title);
                return Ok(responseBody);
            }
            catch (Exception ex)
            {
                return BadRequest("Oops, something went wrong! " + ex.Message);
            }
        }
        
        [HttpGet("/api/v1/books")]
        public async Task<IActionResult> GetMovies()
        {
            try
            {
                var responseBody = await commService.ObtainBooks();
                return Ok(responseBody);
            }
            catch (Exception ex)
            {
                return BadRequest("Oops, something went wrong! " + ex.Message);
            }
        }
        
        [HttpGet("/api/v1/books/rating")]
        public async Task<IActionResult> GetBooksByRating(double min_rating)
        {
            try
            {
                var responseBody = await commService.ObtainBooksByRating(min_rating);
                return Ok(responseBody);
            }
            catch (Exception ex)
            {
                return BadRequest("Oops, something went wrong! " + ex.Message);
            }
        }
    }
}
