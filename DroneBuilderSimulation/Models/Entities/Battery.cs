namespace DroneBuilderSimulation.Models.Entities
{
    public class Battery : ComponentBase
    {
        // e.g., 4S (14.8v) or 6S (22.2v)
        public int CellCountS { get; set; }

        // Fuel capacity (e.g., 1300, 1500)
        public int CapacityMah { get; set; }

        // Discharge rate (determines Max Amps output)
        public int CRating { get; set; }
    }
}