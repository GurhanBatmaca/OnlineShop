using Entity;
using Microsoft.EntityFrameworkCore;

namespace Data.Concrete.EfCore
{
    public class ShopContext: DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options):base(options)
        {           
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(e=>e.Id);
            modelBuilder.Entity<Product>().Property(e=>e.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(e=>e.Price).IsRequired();
            modelBuilder.Entity<Product>().Property(e=>e.Description).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Product>().Property(e=>e.DateAdded).HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Category>().HasKey(e=>e.Id);
            modelBuilder.Entity<Category>().Property(e=>e.Name).IsRequired().HasMaxLength(50);
            
            modelBuilder.Entity<ProductCategory>().HasKey(e=> new {e.ProductId,e.CategoryId});
        }
    }
}