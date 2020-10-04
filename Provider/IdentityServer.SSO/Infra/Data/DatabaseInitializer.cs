﻿using IdentityServer.SSO.Data.Context;
using IdentityServer.SSO.Options;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace IdentityServer.SSO.Infra.Data
{
    public static class DatabaseInitializer
    {
        public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                IServiceProvider provider = serviceScope.ServiceProvider;

                provider.MigrateDbContext<ApplicationDbContext>();
                provider.MigrateDbContext<PersistedGrantDbContext>();
                provider.MigrateDbContext<ConfigurationDbContext>();

                provider.PersistDefaultClients();
                provider.PersistDefaultIdentityResources();
                provider.PersistDefaultApiScopes();
                provider.PersistDefaultIdentityUser();
                provider.PersistDefaultRoles();
                provider.PersistDefaultUserRolesRelation();
            }

            return app;
        }

        #region Persist

        private static void PersistDefaultClients(this IServiceProvider provider)
        {
            var context = provider.GetRequiredService<ConfigurationDbContext>();

            if (!context.Clients.Any())
            {
                foreach (var client in DefaultConfigurations.GetClients())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }
        }

        private static void PersistDefaultIdentityResources(this IServiceProvider provider)
        {
            var context = provider.GetRequiredService<ConfigurationDbContext>();

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in DefaultConfigurations.GetIdentityResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }

        private static void PersistDefaultApiScopes(this IServiceProvider provider)
        {
            var context = provider.GetRequiredService<ConfigurationDbContext>();

            if (!context.ApiScopes.Any())
            {
                foreach (var apiScope in DefaultConfigurations.GetApiScopes())
                {
                    context.ApiScopes.Add(apiScope.ToEntity());
                }
                context.SaveChanges();
            }
        }

        private static void PersistDefaultIdentityUser(this IServiceProvider provider)
        {
            var context = provider.GetRequiredService<ApplicationDbContext>();
            var userManger = provider.GetRequiredService<UserManager<IdentityUser>>();

            if (!context.Users.Any())
            {
                var user = new IdentityUser("admin");
                userManger.CreateAsync(user).GetAwaiter().GetResult();
                userManger.AddClaimsAsync(user, DefaultConfigurations.GetPrimaryClaims()).GetAwaiter().GetResult();
                userManger.AddPasswordAsync(user, "password").GetAwaiter().GetResult();
            }
        }

        private static void PersistDefaultRoles(this IServiceProvider provider)
        {
            var context = provider.GetRequiredService<ApplicationDbContext>();
            var roleManger = provider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] rolesNames = { "Admin", "Guest", "Operator" };

            if (!context.Roles.Any())
            {
                foreach (var role in rolesNames)
                {
                    roleManger.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                }
            }
        }

        private static void PersistDefaultUserRolesRelation(this IServiceProvider provider)
        {
            var context = provider.GetRequiredService<ApplicationDbContext>();
            var userManger = provider.GetRequiredService<UserManager<IdentityUser>>();
            
            if (!context.UserRoles.Any())
            {
                var user = userManger.FindByNameAsync("admin").GetAwaiter().GetResult();

                if (user != null)
                {
                    userManger.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
                }
            }
        }

        #endregion

        private static void MigrateDbContext<TDbContext>(this IServiceProvider provider)
            where TDbContext : DbContext
        {
            provider.GetRequiredService<TDbContext>().Database.Migrate();
        }
    }
}
