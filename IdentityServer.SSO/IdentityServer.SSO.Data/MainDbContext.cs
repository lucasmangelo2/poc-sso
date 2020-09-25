using IdentityServer.SSO.Model;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.SSO.Data
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationClaim> ApplicationClaims { get; set; }
    }
}
