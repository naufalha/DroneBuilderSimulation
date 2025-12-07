using System.ComponentModel.DataAnnotations;

namespace DroneBuildSimulation.Entities;

public class Product
{
    public int Id { get; set; }
    public ComponentType Type { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;

    public decimal Price { get; set; }
    public decimal Weight { get; set; }

    // Spesifikasi Khusus (Nullable)
    public decimal? Range { get; set; }   //entryy hanya range hanya untuk tipe reciver,VTX   
    public int? Capacity { get; set; }     // hanya untuk battery
    public int? Rating { get; set; }        // hanya untuk motor dan batteryy
    public string? Size { get; set; }        

    public string? ImageUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}