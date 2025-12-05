namespace DroneBuilderSimulation.Models.Entities
{
    public class RadioReceiver : ComponentBase
    {
        // e.g., ELRS, TBS Crossfire
        public string Protocol { get; set; } = string.Empty;

        // e.g., 2.4 or 915Mhz
        public double FrequencyGhz { get; set; }
    }
}