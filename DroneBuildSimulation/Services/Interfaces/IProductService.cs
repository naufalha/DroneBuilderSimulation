using DroneBuildSimulation.DTOs.Requests;
using DroneBuildSimulation.DTOs.Responses;
using DroneBuildSimulation.Results;

namespace DroneBuildSimulation.Services.Interfaces;

public interface IProductService
{
    Task<ServiceResult<List<ProductResponse>>> GetAllAsync();
    Task<ServiceResult<ProductResponse>> GetByIdAsync(int id);
    Task<ServiceResult<ProductResponse>> CreateAsync(CreateProductRequest request);
    Task<ServiceResult<bool>> DeleteAsync(int id);
    Task<ServiceResult<ProductResponse>> UpdateAsync(UpdateProductRequest request);
}