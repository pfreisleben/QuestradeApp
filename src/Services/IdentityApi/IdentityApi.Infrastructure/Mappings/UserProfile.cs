using AutoMapper;
using IdentityApi.Domain.Entites;
using Shared.Requests;
using Shared.Responses;

namespace IdentityApi.Infrastructure.Mappings
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