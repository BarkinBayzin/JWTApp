using JWTAppBackOffice.Core.Domain;
using JWTAppBackOffice.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace JWTAppBackOffice.Persistance.Context
{
    public class JWTContext:DbContext
    {
        public JWTContext(DbContextOptions<JWTContext> options) : base(options) { }

        public DbSet<Product> Products 
        {
            get => this.Set<Product>();
        }
        public DbSet<Category> Categories => this.Set<Category>();
        public DbSet<AppUser> AppUsers => this.Set<AppUser>();
        public DbSet<AppRole> AppRoles => this.Set<AppRole>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)  
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
