using bomoseries_Series_api.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bomoseries_Series_api.Services
{
    public interface ICommunicationService
    {
        Task<List<SeriesDTO>> ObtainSepcificSeries(string seriesTitle);
        Task<List<SeriesDTO>> GetSeriesByRating(double min_rating);
        Task<List<SeriesDTO>> ObtainRandomSeries();
    }
}
