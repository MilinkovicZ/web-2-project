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
            modelBuilder.HasIndex(x => x.Name).IsUnique();
            modelBuilder.Property(x => x.Price).IsRequired();
            modelBuilder.Property(x => x.Amount).IsRequired();
            modelBuilder.Property(x => x.Description).HasMaxLength(200);
        }
    }
}