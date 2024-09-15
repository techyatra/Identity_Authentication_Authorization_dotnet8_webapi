using IdentityAuthenticationandAuthorization.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityAuthenticationandAuthorization.Context
{
    public class IdentityDatabaseContext: IdentityDbContext
    {
        public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> dbOptions): base(dbOptions)
        {
                
        }
        public DbSet<TestUser> TestUsers { get; set; }
    }
}
