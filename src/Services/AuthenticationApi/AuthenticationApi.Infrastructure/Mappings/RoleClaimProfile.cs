using AuthenticationApi.Domain.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shared.Requests;
using Shared.Responses;

namespace AuthenticationApi.Infrastructure.Mappings
{
    public class RoleClaimProfile : Profile
    {
        public RoleClaimProfile()
        {
            CreateMap<RoleClaimResponse, AppRoleClaim>()
                .ForMember(nameof(AppRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(IdentityRoleClaim<string>.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();

            CreateMap<RoleClaimRequest, AppRoleClaim>()
                .ForMember(nameof(AppRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(AppRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();
        }
    }
}