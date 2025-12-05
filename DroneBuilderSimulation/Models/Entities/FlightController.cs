namespace DroneBuilderSimulation.Models.Entities
{
    public class FlightController : ComponentBase
    {
        // e.g., F405, F722
        public string Processor { get; set; } = string.Empty;

        // Constraint: Must be > Motor Max Draw
        public int EscCurrentRatingAmps { get; set; }

        // e.g., Betaflight, INAV
        public string Firmware { get; set; } = string.Empty;
    }
}