using IdentityServer.SSO.Infra.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IdentityServer.SSO.IoC;
using IdentityServer.SSO.Infra.Data;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using IdentityServer.SSO.Infra;
using IdentityServer.SSO.Data.Context;

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

            services.AddDbContext<ApplicationDbContext>(options => ConfigureNpgsqlDbContext(options, "SSOIdentityUser"));

            services.AddIdentity<IdentityUser, IdentityRole>(options => {

                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
               .AddAspNetIdentity<IdentityUser>()
               .AddDeveloperSigningCredential()
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

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            services.AddSingleton(mappingConfig.CreateMapper());
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
