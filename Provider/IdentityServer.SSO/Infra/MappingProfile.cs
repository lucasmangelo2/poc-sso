using AutoMapper;
using IdentityModel;
using IdentityServer.SSO.Infra.Extensions;
using IdentityServer.SSO.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer.SSO.Infra
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IdentityUser, UserViewModel>().ReverseMap();

            //CreateMap<IdentityServer4.Models.Client, IdentityServer4.EntityFramework.Entities.Client>()
            //    .ForMember(src => src.AllowedScopes,
            //        dst => dst.MapFrom(x => x.AllowedScopes
            //                                .Select(y => new IdentityServer4.EntityFramework.Entities.ClientScope() { Scope = y }).ToList())
            //    )
            //    .ForMember(src => src.Properties,
            //        dst => dst.MapFrom(x => x.Properties
            //                                .Select(y => new IdentityServer4.EntityFramework.Entities.ClientProperty() { Value = y.Value, Key = y.Key }).ToList())
            //    )
            //    .ForMember(src => src.AllowedGrantTypes,
            //        dst => dst.MapFrom(x => x.AllowedGrantTypes
            //                                .Select(y => new IdentityServer4.EntityFramework.Entities.ClientGrantType() { GrantType = y }).ToList())
            //    )
            //    .ForMember(src => src.RedirectUris,
            //        dst => dst.MapFrom(x => x.RedirectUris
            //                                .Select(y => new IdentityServer4.EntityFramework.Entities.ClientRedirectUri() { RedirectUri = y }).ToList())
            //    )
            //    .ForMember(src => src.PostLogoutRedirectUris,
            //        dst => dst.MapFrom(x => x.PostLogoutRedirectUris
            //                                .Select(y => new IdentityServer4.EntityFramework.Entities.ClientPostLogoutRedirectUri() { PostLogoutRedirectUri = y }).ToList())
            //    )
            //    .ForMember(src => src.AllowedCorsOrigins,
            //        dst => dst.MapFrom(x => x.AllowedCorsOrigins
            //                                .Select(y => new IdentityServer4.EntityFramework.Entities.ClientCorsOrigin() { Origin = y }).ToList())
            //    );

            CreateMap<ApplicationViewModel, IdentityServer4.Models.Client>()
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

            CreateMap<IdentityServer4.Models.Client, ApplicationViewModel>()
                .ForMember(src => src.AllowedGrantTypes, dst => dst.MapFrom(x => x.AllowedGrantTypes.JoinListToString(",")))
                .ForMember(src => src.RedirectUris, dst => dst.MapFrom(x => x.RedirectUris.JoinListToString(",")))
                .ForMember(src => src.PostLogoutRedirectUris, dst => dst.MapFrom(x => x.PostLogoutRedirectUris.JoinListToString(",")))
                .ForMember(src => src.ClientSecrets, dst => dst.MapFrom(x => x.ClientSecrets.Select(x => x.Description).ToList().JoinListToString(",")));
        }
    }
}
