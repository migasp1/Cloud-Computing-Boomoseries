﻿using System.Net.Http;
using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Services
{
    public interface IUserPreferencesService
    {
        Task<string> GetFavoriteWatchables(int id);
    }
}
