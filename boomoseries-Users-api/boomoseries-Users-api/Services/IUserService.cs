using boomoseries_Users_api.Entities;

namespace boomoseries_Users_api.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        User GetById(int id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }
}
