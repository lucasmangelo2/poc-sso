using IdentityServer.SSO.Data.Context;
using IdentityServer.SSO.Infra.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IdentityServer.SSO.IoC;
using IdentityServer4.EntityFramework.DbContexts;
using System.Linq;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer.SSO.Infra.Data;

namespace IdentityServer.SSO
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<MainDbContext>(options => ConfigureNpgsqlDbContext(options, "SSOIdentityUser"));

            services.AddIdentityServer()
               .AddDeveloperSigningCredential()
               .AddTestUsers(DefaultConfigurations.GetUsers())
               .AddConfigurationStore(options =>
               {
                   options.ConfigureDbContext = options => ConfigureNpgsqlDbContext(options, "SSOIdentityConfiguration");
               })
               .AddOperationalStore(options =>
               {
                   options.ConfigureDbContext = options => ConfigureNpgsqlDbContext(options, "SSOIdentityPersistedGrant");

                   options.EnableTokenCleanup = true;
                   options.TokenCleanupInterval = 3600;
               });

            services.AddCors(c =>
            {
                c.AddPolicy("api", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddSameSiteCookiePolicy();
            services.RegisterServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseCors("api");

            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.InitializeDatabase();
        }

        #region Utils

        private DbContextOptionsBuilder ConfigureNpgsqlDbContext(DbContextOptionsBuilder options, string connectionName)
        {
            string connectionString = Configuration.GetConnectionString(connectionName);
            var migrationsAssembly = this.GetType().Assembly.GetName().Name;

            return options.UseNpgsql(connectionString, c => c.MigrationsAssembly(migrationsAssembly));
        }

        #endregion
    }
}
