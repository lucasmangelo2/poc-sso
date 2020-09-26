using IdentityServer.SSO.Data.Context.Sync;
using IdentityServer.SSO.Data.Interfaces.Context;
using IdentityServer.SSO.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IdentityServer.SSO.Data.Context
{
    public class MainDbContext : DbContext, IMainDbContext
    {
        public MainDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            ClaimSync.SyncClaims(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Claim> ClaimTypes { get; set; }
    }
}
