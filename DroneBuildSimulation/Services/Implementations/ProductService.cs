using AutoMapper;
using DroneBuildSimulation.DTOs.Requests;
using DroneBuildSimulation.DTOs.Responses;
using DroneBuildSimulation.Entities;
using DroneBuildSimulation.Repositories.Interfaces;
using DroneBuildSimulation.Results;
using DroneBuildSimulation.Services.Interfaces;

namespace DroneBuildSimulation.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<ProductResponse>>> GetAllAsync()
    {
        var products = await _repo.GetAllAsync();
        var dtos = _mapper.Map<List<ProductResponse>>(products);
        return ServiceResult<List<ProductResponse>>.Ok(dtos);
    }

    public async Task<ServiceResult<ProductResponse>> GetByIdAsync(int id)
    {
        var product = await _repo.GetByIdAsync(id);
        if (product == null) return ServiceResult<ProductResponse>.Fail("Produk tidak ditemukan");

        var dto = _mapper.Map<ProductResponse>(product);
        return ServiceResult<ProductResponse>.Ok(dto);
    }

    public async Task<ServiceResult<ProductResponse>> CreateAsync(CreateProductRequest request)
    {
        // Validasi Bisnis Logic
        if (request.Price < 0) return ServiceResult<ProductResponse>.Fail("Harga tidak boleh minus");

        var entity = _mapper.Map<Product>(request);
        
        await _repo.AddAsync(entity);
        await _repo.SaveAsync();

        var response = _mapper.Map<ProductResponse>(entity);
        return ServiceResult<ProductResponse>.Ok(response, "Produk berhasil dibuat");
    }

    public async Task<ServiceResult<bool>> DeleteAsync(int id)
    {
        var product = await _repo.GetByIdAsync(id);
        if (product == null) return ServiceResult<bool>.Fail("Produk tidak ditemukan");

        await _repo.DeleteAsync(product);
        await _repo.SaveAsync();

        return ServiceResult<bool>.Ok(true, "Produk dihapus");
    }
public async Task<ServiceResult<ProductResponse>> UpdateAsync(UpdateProductRequest request)
{
    // 1. Ambil data lama dari Database (Tracking ON secara default via FindAsync)
    var existingProduct = await _repo.GetByIdAsync(request.Id);
    
    if (existingProduct == null) 
        return ServiceResult<ProductResponse>.Fail("Produk tidak ditemukan");

    // 2. Timpa data lama dengan data dari Form (AutoMapper magic)
    _mapper.Map(request, existingProduct); 
    // Saat baris ini jalan, property di variable 'existingProduct' berubah sesuai input user

    // 3. Beritahu Repository untuk update
    await _repo.UpdateAsync(existingProduct);
    
    // 4. Commit ke Database
    await _repo.SaveAsync();

    var response = _mapper.Map<ProductResponse>(existingProduct);
    return ServiceResult<ProductResponse>.Ok(response, "Produk berhasil diupdate");
}
}