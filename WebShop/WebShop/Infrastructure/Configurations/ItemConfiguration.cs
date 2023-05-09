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
            modelBuilder.HasOne(x => x.Order).WithMany(x => x.Items).HasForeignKey(x => x.OrderId);
        }
    }
}