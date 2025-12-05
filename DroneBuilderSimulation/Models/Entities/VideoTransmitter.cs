namespace DroneBuilderSimulation.Models.Entities
{
    public class VideoTransmitter : ComponentBase
    {
        // Power consumption
        public double PowerMw { get; set; }

        // Theoretical range
        public double MaxRangeKm { get; set; }

        // DJI/Walksnail vs Analog
        public bool IsDigital { get; set; }
    }
}