using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DroneBuilderSimulation.Models.Entities;

namespace DroneBuilderSimulation.Models.Configurations
{
    public class MotorConfiguration : IEntityTypeConfiguration<Motor>
    {
        public void Configure(EntityTypeBuilder<Motor> builder)
        {
            // Base properties
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Price)
                .HasPrecision(18, 2); // Standard for currency

            // Physics constraints
            builder.Property(m => m.Kv)
                .IsRequired();
                
            // We can add data seeding here later if needed
        }
    }
}