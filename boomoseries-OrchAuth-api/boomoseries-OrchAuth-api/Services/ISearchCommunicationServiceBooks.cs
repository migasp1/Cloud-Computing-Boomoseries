using boomoseries_OrchAuth_api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Services
{
    public interface ISearchCommunicationServiceBooks
    {
        Task<List<BookDTO>> ObtainSpecificBook(string type, string bookTitle);
        Task<List<BookDTO>> ObtainRandomBooks(string type);
        Task<List<BookDTO>> ObtainBooksByRating(string type, double min_rating);
    }
}
