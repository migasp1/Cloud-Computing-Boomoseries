using boomoseries_Search_api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boomoseries_Search_api.Services
{
    public interface ICommunicationServiceSeries
    {
        Task<object> ObtainSepcificSeries(string seriesTitle);
        Task<object> GetSeriesByRating(double min_rating);
        Task<object> ObtainRandomSeries();
    }
}
