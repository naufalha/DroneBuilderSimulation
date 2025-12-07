using DroneBuildSimulation.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DroneBuildSimulation.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Brand).IsRequired().HasMaxLength(50);
        
        // SQLite support decimal, tapi define precision tetap best practice
        builder.Property(x => x.Price).HasPrecision(18, 2);
        builder.Property(x => x.Weight).HasPrecision(10, 2);

        // Simpan Enum sebagai String agar mudah dibaca di DB Browser for SQLite
        builder.Property(x => x.Type).HasConversion<string>(); 
    }
}