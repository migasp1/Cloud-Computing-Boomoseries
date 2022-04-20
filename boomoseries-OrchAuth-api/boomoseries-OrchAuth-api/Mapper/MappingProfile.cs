using AutoMapper;
using boomoseries_OrchAuth_api.Entities;
using boomoseries_OrchAuth_api.Models;

namespace boomoseries_OrchAuth_api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserBookPreferenceModel, UserBookPreferenceDTO>();
            CreateMap<UserWatchablePreferenceModel, UserWatchablePreferenceDTO>();
        }
    }
}
