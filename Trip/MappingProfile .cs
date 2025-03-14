using AutoMapper;
using Trip.Models;
using Trip.Models.Extra.DTOs;

namespace Trip
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
