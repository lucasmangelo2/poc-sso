using AutoMapper;
using IdentityModel;
using IdentityServer.SSO.Infra.Extensions;
using IdentityServer.SSO.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;

namespace IdentityServer.SSO.Infra
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IdentityUser, UserViewModel>().ReverseMap();

            CreateMap<ClientViewModel, IdentityServer4.Models.Client>()
                .ForMember(src => src.RedirectUris,
                            dst => dst.MapFrom(x => x.RedirectUris.Split(',', StringSplitOptions.None))
                )
                .ForMember(src => src.PostLogoutRedirectUris,
                            dst => dst.MapFrom(x => x.PostLogoutRedirectUris.Split(',', StringSplitOptions.None))
                )
                .ForMember(src => src.AllowedGrantTypes,
                            dst => dst.MapFrom(x => x.AllowedGrantTypes.Split(',', StringSplitOptions.None))
                )
                .ForMember(src => src.ClientSecrets,
                            dst => dst.MapFrom(x => x.ClientSecrets
                                                    .Split(',', StringSplitOptions.None)
                                                    .Select(y => new IdentityServer4.Models.Secret(y.ToSha256(), y, null))
                                                    .ToList())
            );

            CreateMap<IdentityServer4.Models.Client, ClientViewModel>()
                .ForMember(src => src.AllowedGrantTypes, dst => dst.MapFrom(x => x.AllowedGrantTypes.JoinListToString(",")))
                .ForMember(src => src.RedirectUris, dst => dst.MapFrom(x => x.RedirectUris.JoinListToString(",")))
                .ForMember(src => src.PostLogoutRedirectUris, dst => dst.MapFrom(x => x.PostLogoutRedirectUris.JoinListToString(",")))
                .ForMember(src => src.ClientSecrets, dst => dst.MapFrom(x => x.ClientSecrets.Select(x => x.Description).ToList().JoinListToString(",")));

            CreateMap<ClaimViewModel, Claim>().ReverseMap();
        }
    }
}
