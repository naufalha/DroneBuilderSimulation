namespace DroneBuilderSimulation.Models.Entities
{
    public class Motor : ComponentBase
    {
        public int Kv { get; set; } 

        public int StatorSize { get; set; } 

        public double MaxContinuousCurrentAmps { get; set; } 

        public double MaxPowerWatts { get; set; }

        public int RequiredCount { get; set; } = 4; 
    }
}