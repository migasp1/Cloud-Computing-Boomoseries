using boomoseries_Books_api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boomoseries_Books_api.Services
{
    public interface ICommunicationService
    {
        Task<object> ObtainSpecificBook(string bookTitle);
        Task<object> ObtainRandomBooks();
        Task<object> ObtainBooksByRating(double min_rating);
    }
}
