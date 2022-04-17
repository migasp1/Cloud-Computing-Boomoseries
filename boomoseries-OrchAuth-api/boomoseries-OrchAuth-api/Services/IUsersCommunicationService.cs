using boomoseries_OrchAuth_api.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Services
{
    public interface IUsersCommunicationService
    {
        Task<HttpResponseMessage> RegisterUser(RegisterModel registerModel);
    }
}
