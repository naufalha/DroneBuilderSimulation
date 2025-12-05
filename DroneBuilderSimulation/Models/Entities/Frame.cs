namespace DroneBuilderSimulation.Models.Entities
{
    public class Frame : ComponentBase
    {
        //[cite_start]// e.g., "5 inch", "3 inch" [cite: 53]
        public string SizeCategory { get; set; } = string.Empty;

        //[cite_start]// e.g., "Carbon Fiber" [cite: 54]
        public string Material { get; set; } = string.Empty;

        public double WheelbaseMm { get; set; }

        //[cite_start]// Constraint for validation logic later [cite: 56]
        public int MaxPropSizeInch { get; set; } 
    }
}