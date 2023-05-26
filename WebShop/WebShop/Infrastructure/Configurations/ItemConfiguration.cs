using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebShop.Models;

namespace WebShop.Infrastructure.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> modelBuilder)
        {
            modelBuilder.HasKey(x => x.Id);
            modelBuilder.HasOne(x => x.Order).WithMany(x => x.Items).HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.HasOne(x => x.Product).WithMany(x => x.Items).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Property(x => x.ProductAmount).IsRequired();
            modelBuilder.Property(x => x.CurrentPrice).IsRequired();
        }
    }
}