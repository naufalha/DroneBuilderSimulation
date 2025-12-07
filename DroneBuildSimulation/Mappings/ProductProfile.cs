using AutoMapper;
using DroneBuildSimulation.DTOs.Requests;
using DroneBuildSimulation.DTOs.Responses;
using DroneBuildSimulation.Entities;
using System.Globalization;

namespace DroneBuildSimulation.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // Entity ke Response
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.PriceFormatted, opt => opt.MapFrom(src => src.Price.ToString("C", new CultureInfo("id-ID"))));

        // Request ke Entity
        CreateMap<CreateProductRequest, Product>();

        CreateMap<Product, UpdateProductRequest>();

       // 3. Update Request ke Entity (PENTING UNTUK EDIT!) 👈 Pastikan ini ada
        CreateMap<UpdateProductRequest, Product>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); 
            // Opsional: Baris Condition di atas memastikan null tidak menimpa data (biasanya aman dihapus kalau validation ketat)
    }
}