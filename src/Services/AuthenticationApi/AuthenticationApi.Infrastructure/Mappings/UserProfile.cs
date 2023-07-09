using AuthenticationApi.Domain.Entites;
using AutoMapper;
using Shared.Requests;
using Shared.Responses;

namespace AuthenticationApi.Infrastructure.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserResponse, AppUser>().ReverseMap();
            CreateMap<UserResponse, UserRequest>().ReverseMap();
        }
    }
}