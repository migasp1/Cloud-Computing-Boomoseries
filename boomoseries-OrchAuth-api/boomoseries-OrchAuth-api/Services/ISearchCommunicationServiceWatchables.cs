using boomoseries_OrchAuth_api.Helpers;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Services
{
    public interface ISearchCommunicationServiceWatchables
    {
        Task<List<WatchableDTO>> ObtainSepcificWatchables(string type, string watchable_title);
        Task<List<WatchableDTO>> GetWatchblesByRating(string type, double minRating);
        Task<List<WatchableDTO>> ObtainRandomWatchable(string type);
    }
}
