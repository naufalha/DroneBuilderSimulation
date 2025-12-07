using DroneBuildSimulation.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DroneBuildSimulation.DTOs.Requests;

public class UpdateProductRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public ComponentType Type { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Brand { get; set; } = string.Empty;

    public decimal Price { get; set; }
    public decimal Weight { get; set; }

    // Optional Specs
    public decimal? Range { get; set; }
    public int? Capacity { get; set; }
    public int? Rating { get; set; }
    public string? Size { get; set; }

    // Gambar
    public IFormFile? ImageFile { get; set; } // Upload baru (opsional)
    public string? ImageUrl { get; set; }     // Path gambar hasil upload (untuk DB)
}