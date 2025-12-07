namespace DroneBuildSimulation.DTOs.Responses;

public class ProductResponse
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty; // String enum
    public string Name { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    
    public decimal Price { get; set; }
    public string PriceFormatted { get; set; } = string.Empty;
    
    public decimal Weight { get; set; }
    
    // Details
    public decimal? Range { get; set; }
    public int? Capacity { get; set; }
    public int? Rating { get; set; }
    public string? Size { get; set; }
    public string? ImageUrl { get; set; }
}