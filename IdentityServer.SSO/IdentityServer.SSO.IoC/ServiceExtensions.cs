using IdentityServer.SSO.Business;
using IdentityServer.SSO.Business.Interfaces;
using IdentityServer.SSO.Data.Context;
using IdentityServer.SSO.Data.Interfaces.Context;
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

            services.AddScoped<IClaimBusiness, ClaimBusiness>();
            services.AddScoped<IApplicationBusiness, ApplicationBusiness>();
            services.AddScoped<IUserRepository, UserRepository>();

            #endregion

            #region Respository

            services.AddScoped<IClaimRepository, ClaimRepository>();
            services.AddScoped<IApplicationRepository, ApplcationRepository>();
            services.AddScoped<IApplicationClaimRepository, ApplicationClaimRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserClaimRepository, UserClaimRepository>();

            #endregion

            #region Context

            services.AddTransient<IMainDbContext, MainDbContext>();

            #endregion

            return services;
        }
    }
}
