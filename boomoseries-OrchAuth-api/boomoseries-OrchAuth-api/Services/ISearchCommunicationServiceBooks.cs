using boomoseries_OrchAuth_api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Services
{
    public interface ISearchCommunicationServiceBooks
    {
        Task<object> ObtainSpecificBook(string type, string bookTitle);
        Task<object> ObtainRandomBooks(string type);
        Task<object> ObtainBooksByRating(string type, double min_rating);
    }
}
