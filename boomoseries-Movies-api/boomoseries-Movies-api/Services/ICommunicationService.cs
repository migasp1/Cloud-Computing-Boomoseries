using boomoseries_Movies_api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boomoseries_Movies_api.Services
{
    public interface ICommunicationService
    {
        Task<List<MovieDTO>> ObtainSepcificMovie(string movieTitle);
        Task<string> ObtainMovies();
        Task<List<MovieDTO>> GetMoviesByRating(double min_rating);
    }
}
