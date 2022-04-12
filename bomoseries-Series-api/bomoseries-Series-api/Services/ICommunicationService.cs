using bomoseries_Series_api.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bomoseries_Series_api.Services
{
    public interface ICommunicationService
    {
        Task<List<SerieDTO>> ObtainSepcificSeries(string seriesTitle);
        Task<List<SerieDTO>> GetSeriesByRating(double min_rating);
        Task<List<SerieDTO>> ObtainRandomSeries();
    }
}
