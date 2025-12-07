using DroneBuilderSimulation.Models.Entities;

namespace DroneBuilderSimulation.Services.Interfaces
{
    public interface IDronePhysicsService
    {
        DroneBuild CalculateStats(DroneBuild build);
    }
}
