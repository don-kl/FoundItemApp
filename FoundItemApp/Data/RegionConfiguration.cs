using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FoundItemApp.Models;

namespace FoundItemApp.Data
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable("Regions");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Borders);
            builder.HasMany(x => x.Items).WithOne(x => x.Region).HasForeignKey(x => x.RegionId);
        }
    }
}
