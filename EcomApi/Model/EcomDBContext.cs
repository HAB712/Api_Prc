using Microsoft.EntityFrameworkCore;

namespace EcomApi.Model
{
    public class EcomDBContext : DbContext
    {
        public EcomDBContext(DbContextOptions<EcomDBContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
