using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC.Models.Identity;

namespace MVC.Models.Market
{
    public class MarketContext : IdentityDbContext<AppUser, AppUserRole, int>
    {
        public MarketContext(DbContextOptions<MarketContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryID);
            base.OnModelCreating(builder);
        }
    }
}
