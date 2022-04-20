using boomoseries_OrchAuth_api.Entities;
using boomoseries_OrchAuth_api.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Services
{
    public interface IUserPreferencesService
    {
        Task<string> GetFavoriteWatchables(int id);
        Task<string> AddFavoriteWatchables(UserWatchablePreferenceDTO favWatchable);
        Task<string> AddFavoriteBook(UserBookPreferenceDTO favBook);
    }
}
