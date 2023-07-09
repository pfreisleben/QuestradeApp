using AuthenticationApi.Domain.Entites;
using AutoMapper;
using Shared.Responses;

namespace AuthenticationApi.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, AppRole>().ReverseMap();
        }
    }
}