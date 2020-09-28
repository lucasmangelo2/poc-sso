using IdentityModel;
using IdentityServer.SSO.Enums;
using IdentityServer.SSO.Model;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.SSO.Data.Context.Sync
{
    public class ClaimSync
    {
        public static void SyncClaims(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Claim>()
                .HasData(new Claim()
                {
                    Id = 1,
                    Name = "insert",
                    Type = ClaimType.Application
                });

            modelBuilder
                .Entity<Claim>()
                .HasData(new Claim()
                {
                    Id = 2,
                    Name = "update",
                    Type = ClaimType.Application
                });

            modelBuilder
                .Entity<Claim>()
                .HasData(new Claim()
                {
                    Id = 3,
                    Name = "delete",
                    Type = ClaimType.Application
                });

            modelBuilder
                .Entity<Claim>()
                .HasData(new Claim()
                {
                    Id = 5,
                    Name = "get",
                    Type = ClaimType.Application
                });


            modelBuilder
                .Entity<Claim>()
                .HasData(new Claim()
                {
                    Id = 6,
                    Name = JwtClaimTypes.Name,
                    Type = ClaimType.User
                });

            modelBuilder
                .Entity<Claim>()
                .HasData(new Claim()
                {
                    Id = 7,
                    Name = JwtClaimTypes.Email,
                    Type = ClaimType.User
                });
        }
    }
}
