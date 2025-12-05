namespace DroneBuilderSimulation.Models.Entities
{
   // Abstract base class to avoid code repetition 
    public abstract class ComponentBase
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty;
        
        public string Manufacturer { get; set; } = string.Empty;
        
        // Use decimal for money/price [cite: 48]
        public decimal Price { get; set; }
        
        // Critical for Flight Time calculations [cite: 48]
        public double WeightGrams { get; set; } 
        
        public string ImageUrl { get; set; } = string.Empty;
    }
}