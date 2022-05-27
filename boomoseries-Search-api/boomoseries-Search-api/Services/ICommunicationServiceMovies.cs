using boomoseries_Search_api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boomoseries_Search_api.Services
{
    public interface ICommunicationServiceMovies
    {
        Task<object> ObtainSepcificMovie(string movieTitle);
        Task<object> GetMoviesByRating(double min_rating);
        Task<object> ObtainRandomMovies();
    }
}
