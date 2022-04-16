using AutoMapper;
using boomoseries_Users_api.Entities;
using boomoseries_Users_api.Models;

namespace boomoseries_Users_api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
        }
    }
}