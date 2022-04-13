using boomoseries_Search_api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boomoseries_Search_api.Services
{
    public interface ICommunicationServiceMovies
    {
        Task<List<MovieDTO>> ObtainSepcificMovie(string movieTitle);
        Task<List<MovieDTO>> GetMoviesByRating(double min_rating);
        Task<List<MovieDTO>> ObtainRandomMovies();
    }
}
