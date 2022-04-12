using boomosseries_GoodReads_api.Data;
using boomosseries_GoodReads_api.DTOs;
using boomosseries_GoodReads_api.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boomosseries_GoodReads_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GoodReadsController : Controller
    {
        private readonly DataContext dataContext;
        public GoodReadsController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        [HttpGet("books")]
        public async Task<IActionResult> GetBooks(double? min_rating)
        {
            var books = await dataContext.Books.ToListAsync();
            List<BooksDTO> bookDTO = new();
            foreach (var book in books)
            {
                bookDTO.Add(GoodReadsMapper.MapToDTO(book));
            }
            if (min_rating == null)
            {
                return Ok(bookDTO);
            }
            else if (min_rating != null)
            {
                var result = bookDTO.Where(book => book.Rating >= min_rating);
                if (!result.Any())
                {
                    return NotFound("There are no books with more than rating" + min_rating + ".");
                }
                else
                {
                    return Ok(result);
                }
            }
            return BadRequest("Something went wrong");
        }

        [HttpGet("books/{book_title}")]
        public async Task<IActionResult> GetSpecificBook(string book_title)
        {
            var books = await dataContext.Books.ToListAsync();
            var book = books.FirstOrDefault(book => book.Title.ToLower().Contains(book_title.ToLower()));
            if (book == null)
            {
                return NotFound("This book does not exist");
            }
            else
            {
                BooksDTO booksDTO = GoodReadsMapper.MapToDTO(book);
                return Ok(booksDTO);
            }
        }

        [HttpGet("books/random")]
        public async Task<IActionResult> GetRandomMovies()
        {
            var dbSet = dataContext.Books;

            var totalBooks = dbSet.Count();

            Random random = new();
            List<BooksDTO> books = new List<BooksDTO>();            
            
            for (int i = 0; i < 3; i++)
            {
                int randomId = random.Next(1, totalBooks + 1);
                var randomBook = dbSet.SingleOrDefault(s => s.Id == randomId);
                BooksDTO bookDTO = GoodReadsMapper.MapToDTO(randomBook);
                books.Add(bookDTO);
            }

            if (books.Count == 0)
            {
                return BadRequest("Oops, something went wrong!");
            }
            else
            {
                return Ok(books);
            }
        }
    }
}
