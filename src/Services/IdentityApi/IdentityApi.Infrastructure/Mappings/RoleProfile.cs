using AutoMapper;
using IdentityApi.Domain.Entites;
using Shared.Responses;

namespace IdentityApi.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, AppRole>().ReverseMap();
        }
    }
}