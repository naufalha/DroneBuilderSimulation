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
        // ==========================================
        // 1. DARI DATABASE KE KELUAR (Response)
        // ==========================================
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.PriceFormatted, opt => opt.MapFrom(src => src.Price.ToString("C", new CultureInfo("id-ID"))));

        // ==========================================
        // 2. DARI FORM INPUT KE DATABASE (Request -> Entity)
        // ==========================================
        CreateMap<CreateProductRequest, Product>();
        
        CreateMap<UpdateProductRequest, Product>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // Opsional: Hanya update yang tidak null

        // ==========================================
        // 3. (BARU) DARI RESPONSE KE FORM EDIT 
        // ==========================================
        // Ini dibutuhkan saat Controller menerima 'ProductResponse' dari Service
        // dan ingin menampilkannya kembali ke form Edit sebagai 'UpdateProductRequest'
        CreateMap<ProductResponse, UpdateProductRequest>()
            // Jika ada tipe data yang beda (misal String ke Enum), perlu di-handle:
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<ComponentType>(src.Type)));
    }
}