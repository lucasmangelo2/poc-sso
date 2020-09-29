using AutoMapper;
using IdentityServer.SSO.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.SSO.Infra
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IdentityUser, UserViewModel>().ReverseMap();
        }
    }
}
