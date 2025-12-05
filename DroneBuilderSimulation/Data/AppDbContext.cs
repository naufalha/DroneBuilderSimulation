using Microsoft.EntityFrameworkCore;
using DroneBuilderSimulation.Models.Entities;
using DroneBuilderSimulation.Models.Configurations;

namespace DroneBuilderSimulation.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Register Tables
        public DbSet<Frame> Frames { get; set; }
        public DbSet<Motor> Motors { get; set; }
        public DbSet<Battery> Batteries { get; set; }
        public DbSet<Propeller> Propellers { get; set; }
        public DbSet<FlightController> FlightControllers { get; set; }
        public DbSet<VideoTransmitter> VideoTransmitters { get; set; }
        public DbSet<RadioReceiver> RadioReceivers { get; set; }
        public DbSet<DroneBuild> DroneBuilds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations from the current assembly automatically
            // This finds MotorConfiguration, DroneBuildConfiguration, etc.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}