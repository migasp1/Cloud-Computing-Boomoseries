using boomoseries_Books_api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boomoseries_Books_api.Services
{
    public interface ICommunicationService
    {
        Task<List<BooksDTO>> ObtainSpecificBook(string bookTitle);
        Task<List<BooksDTO>> ObtainBooks();
        Task<List<BooksDTO>> ObtainBooksByRating(double min_rating);
    }
}
