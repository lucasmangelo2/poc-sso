﻿using IdentityServer.SSO.Business;
using IdentityServer.SSO.Business.Interfaces;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer.SSO.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.SSO.IoC
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            #region Business

            services.AddScoped<IClientBusiness, ClientBusiness>();
            services.AddScoped<IUserBusiness, UserBusiness>();

            #endregion

            #region Respository

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            #endregion

            return services;
        }
    }
}
