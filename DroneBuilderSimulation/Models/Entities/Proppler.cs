namespace DroneBuilderSimulation.Models.Entities
{
    public class Propeller : ComponentBase
    {
        public double DiameterInch { get; set; }

        // Higher pitch = more speed, less efficiency
        public double PitchInch { get; set; }

        // Standard is 3 (Tri-blade)
        public int BladeCount { get; set; }
    }
}