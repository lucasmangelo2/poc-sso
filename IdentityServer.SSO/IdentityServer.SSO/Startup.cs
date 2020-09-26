using IdentityServer.SSO.Data.Context;
using IdentityServer.SSO.Infra.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IdentityServer.SSO.IoC;
using System.Reflection;

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

            string connectionString = Configuration.GetConnectionString("SSOConection");
            var migrationsAssembly = this.GetType().Assembly.GetName().Name;

            services.AddDbContext<MainDbContext>(options => 
                options.UseNpgsql(
                    connectionString, 
                    c => c.MigrationsAssembly(migrationsAssembly)
                )
            );

            services.AddIdentityServer()
               .AddDeveloperSigningCredential()
               .AddTestUsers(Config.GetUsers())
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseNpgsql(
                            connectionString,
                            c => c.MigrationsAssembly(migrationsAssembly));
                })
               .AddOperationalStore(options =>
               {
                   options.ConfigureDbContext = builder =>
                       builder.UseNpgsql(connectionString,
                           sql => sql.MigrationsAssembly(migrationsAssembly));

                   // this enables automatic token cleanup. this is optional.
                   options.EnableTokenCleanup = true;
                   options.TokenCleanupInterval = 30;
               });

            services.AddCors(c =>
            {
                c.AddPolicy("api", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddSameSiteCookiePolicy();
            services.RegisterServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
        }
    }
}
