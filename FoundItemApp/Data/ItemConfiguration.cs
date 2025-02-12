using Microsoft.EntityFrameworkCore;
using FoundItemApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundItemApp.Data 
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Item");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
            builder.Property(item => item.Id).IsRequired();
            builder.Property(item => item.Description).IsRequired().HasMaxLength(200);
            builder.Property(item => item.Title).IsRequired().HasMaxLength(50);
            builder.Property(item => item.UserEmail).IsRequired().HasMaxLength(50);
            builder.Property(item => item.Coordinates).IsRequired();
            builder.Property(x => x.Category).IsRequired();
            builder.Property(x => x.DateFound).IsRequired();
            builder.Property(x => x.TimeFound).IsRequired();
        }
    }
}
