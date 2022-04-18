using AutoMapper;
using boomoseries_prefs_api.Entities;
using boomoseries_prefs_api.Models;

namespace boomoseries_prefs_api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserWatchablePreferenceModel, UserWatchablePreference>();
            CreateMap<UserBookPreferenceModel, UserBookPreference>();
        }
    }
}
