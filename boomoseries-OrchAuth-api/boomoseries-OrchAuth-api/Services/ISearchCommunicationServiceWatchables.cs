using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Services
{
    public interface ISearchCommunicationServiceWatchables
    {
        Task<object> ObtainSepcificWatchables(string type, string watchable_title);
        Task<object> GetWatchblesByRating(string type, double minRating);
        Task<object> ObtainRandomWatchable(string type);
    }
}
