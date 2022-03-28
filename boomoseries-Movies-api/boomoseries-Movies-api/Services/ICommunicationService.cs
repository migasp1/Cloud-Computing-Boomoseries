using System.Threading.Tasks;

namespace boomoseries_Movies_api.Services
{
    public interface ICommunicationService
    {
        Task<string> ObtainSepcificMovie(string movieTitle);
    }
}
