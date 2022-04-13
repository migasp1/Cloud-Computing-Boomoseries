using boomoseries_Search_api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boomoseries_Search_api.Services
{
    public interface ICommunicationServiceBooks
    {
        Task<List<BookDTO>> ObtainSpecificBook(string bookTitle);
        Task<List<BookDTO>> ObtainRandomBooks();
        Task<List<BookDTO>> ObtainBooksByRating(double min_rating);
    }
}
