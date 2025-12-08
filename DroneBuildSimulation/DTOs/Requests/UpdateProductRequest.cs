using DroneBuildSimulation.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DroneBuildSimulation.DTOs.Requests;

public class UpdateProductRequest
{
   
    public int Id { get; set; }

    
    public ComponentType Type { get; set; }

       public string Name { get; set; } = string.Empty;


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