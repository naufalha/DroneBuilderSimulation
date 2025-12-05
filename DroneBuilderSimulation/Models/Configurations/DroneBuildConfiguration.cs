using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DroneBuilderSimulation.Models.Entities;

namespace DroneBuilderSimulation.Models.Configurations
{
    public class DroneBuildConfiguration : IEntityTypeConfiguration<DroneBuild>
    {
        public void Configure(EntityTypeBuilder<DroneBuild> builder)
        {
            builder.Property(b => b.BuildName)
                .IsRequired()
                .HasMaxLength(200);

            // Configure Relationships 
            // (EF Core usually guesses these, but being explicit is safer)

            builder.HasOne(b => b.Frame)
                .WithMany()
                .HasForeignKey(b => b.FrameId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a part if it's used in a build

            builder.HasOne(b => b.Motor)
                .WithMany()
                .HasForeignKey(b => b.MotorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Battery)
                .WithMany()
                .HasForeignKey(b => b.BatteryId)
                .OnDelete(DeleteBehavior.Restrict);
            
            
            // ... repeat for other required parts if necessary
        }
    }
}