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

        [HttpGet("/api/v1/Books/{book_title}")]
        public async Task<IActionResult> GetSpecificBook(string book_title)
        {
            try
            {
                var responseBody = await commService.ObtainSpecificBook(book_title);
                if (responseBody.Count == 0) {
                    return NotFound("This Book Doesn't Exist!");
                }
                return Ok(responseBody);
            }
            catch (Exception ex)
            {
                return BadRequest("Oops, something went wrong! ");
            }
        }
        
        [HttpGet("/api/v1/Books/random")]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                var responseBody = await commService.ObtainRandomBooks();
                return Ok(responseBody);
            }
            catch (Exception ex)
            {
                return BadRequest("Oops, something went wrong! ");
            }
        }
        
        [HttpGet("/api/v1/Books")]
        public async Task<IActionResult> GetBooksByRating(double min_rating)
        {
            try
            {
                var responseBody = await commService.ObtainBooksByRating(min_rating);
                return Ok(responseBody);
            }
            catch (Exception ex)
            {
                return BadRequest("Oops, something went wrong! ");
            }
        }
    }
}
