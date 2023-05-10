using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebShop.Models;

namespace WebShop.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> modelBuilder)
        {
            modelBuilder.HasKey(x => x.Id);
            modelBuilder.Property(x => x.DeliveryAddress).IsRequired();
            modelBuilder.Property(x => x.DeliveryTime).IsRequired();
            modelBuilder.Property(x => x.Comment).HasMaxLength(100);
            modelBuilder.Property(x => x.IsDeleted).HasDefaultValue(false);
        }
    }
}