using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebShop.Models;

namespace WebShop.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> modelBuilder)
        {
            modelBuilder.HasKey(x => x.Id);
            modelBuilder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            modelBuilder.Property(x => x.Price).IsRequired();
            modelBuilder.Property(x => x.Amount).IsRequired();
            modelBuilder.Property(x => x.Description).HasMaxLength(200);
            modelBuilder.HasOne(x => x.Seller).WithMany(x => x.Products).HasForeignKey(x => x.SellerId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}