using boomoseries_Books_api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boomoseries_Books_api.Services
{
    public interface ICommunicationService
    {
        Task<string> ObtainSpecificBook(string bookTitle);
        Task<string> ObtainBooks();
        Task<List<BooksDTO>> ObtainBooksByRating(double min_rating);
    }
}
