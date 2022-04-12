using boomoseries_Books_api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boomoseries_Books_api.Services
{
    public interface ICommunicationService
    {
        Task<List<BookDTO>> ObtainSpecificBook(string bookTitle);
        Task<List<BookDTO>> ObtainRandomBooks();
        Task<List<BookDTO>> ObtainBooksByRating(double min_rating);
    }
}
