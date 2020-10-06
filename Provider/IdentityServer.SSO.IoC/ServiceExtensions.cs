using IdentityServer.SSO.Business;
using IdentityServer.SSO.Business.Interfaces;
using IdentityServer.SSO.Data.Context;
using IdentityServer.SSO.Data.Interfaces.Context;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer.SSO.Data.Repository;
using IdentityServer4.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.SSO.IoC
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            #region Business

            services.AddTransient<IProfileService, ProfileService>();

            services.AddScoped<IClientBusiness, ClientBusiness>();
            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<IRoleBusiness, RoleBusiness>();
            services.AddScoped<IWebhookSubscriptionBusiness, WebhookSubscriptionBusiness>();
            services.AddScoped<IWebhookPublisherBusiness, WebhookPublisherBusiness>();
            services.AddScoped<IWebhookSenderBusiness, WebhookSenderBusiness>();

            #endregion

            #region Respository

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWebhookDefinitionRepository, WebhookDefinitionRepository>();
            services.AddScoped<IWebhookSubscriptionRepository, WebhookSubscriptionRepository>();
            services.AddScoped<IWebhookEventRepository, WebhookEventRepository>();
            
            #endregion

            #region Context

            services.AddTransient<IIdentityConfigurationDbContext, IdentityConfigurationDbContext>();
            services.AddTransient<IWebhookDbContext, WebhookDbContext>();

            #endregion

            return services;
        }
    }
}
