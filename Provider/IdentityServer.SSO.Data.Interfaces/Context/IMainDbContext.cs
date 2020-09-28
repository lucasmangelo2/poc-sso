using IdentityServer.SSO.Model;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.SSO.Data.Interfaces.Context
{
    public interface IMainDbContext : IDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<UserClaim> UserClaims { get; set; }
        DbSet<Application> Applications { get; set; }
        DbSet<Claim> ClaimTypes { get; set; }
    }
}
