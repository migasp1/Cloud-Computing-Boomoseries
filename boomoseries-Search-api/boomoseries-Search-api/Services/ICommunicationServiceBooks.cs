using boomoseries_Search_api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boomoseries_Search_api.Services
{
    public interface ICommunicationServiceBooks
    {
        Task<object> ObtainSpecificBook(string bookTitle);
        Task<object> ObtainRandomBooks();
        Task<object> ObtainBooksByRating(double min_rating);
    }
}
