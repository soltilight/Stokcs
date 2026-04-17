using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stocks.Models;
using StocksOperation.Models;

namespace Stocks.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
       
            public ApplicationDBContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
            {

            }
            public DbSet<Stock> Stocks { get; set; }
           public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                      Id = "1",
                    Name="Admin",
                    NormalizedName="ADMIN"
                },
                  new IdentityRole
                  {
                        Id = "2",
                      Name="User",
                      NormalizedName="USER"
                  }
            };
            builder.Entity<IdentityRole>().HasData(roles);

        }
        
    }
}
