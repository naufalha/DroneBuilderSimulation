using DroneBuildSimulation.Entities;
using System.ComponentModel.DataAnnotations;

namespace DroneBuildSimulation.DTOs.Requests;

public class CreateProductRequest
{
    [Required]
    public ComponentType Type { get; set; }

    [Required(ErrorMessage = "Nama produk wajib diisi")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Brand wajib diisi")]
    public string Brand { get; set; } = string.Empty;

    [Range(0, double.MaxValue, ErrorMessage = "Harga harus positif")]
    public decimal Price { get; set; }

    public decimal Weight { get; set; }
    
    // Optional Specs
    public decimal? Range { get; set; }
    public int? Capacity { get; set; }
    public int? Rating { get; set; }
    public string? Size { get; set; }
    public string? ImageUrl { get; set; }
    public IFormFile? ImageFile { get; set; }
}