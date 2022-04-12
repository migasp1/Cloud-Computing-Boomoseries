using boomoseries_Search_api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boomoseries_Search_api.Services
{
    public interface ICommunicationServiceSeries
    {
        Task<List<SerieDTO>> ObtainSepcificSeries(string seriesTitle);
        Task<List<SerieDTO>> GetSeriesByRating(double min_rating);
        Task<List<SerieDTO>> ObtainRandomSeries();
    }
}
