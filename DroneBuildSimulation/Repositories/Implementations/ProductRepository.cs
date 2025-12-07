using DroneBuildSimulation.Data;
using DroneBuildSimulation.Entities;
using DroneBuildSimulation.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DroneBuildSimulation.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllAsync() 
        => await _context.Products.AsNoTracking().OrderByDescending(p => p.CreatedAt).ToListAsync();

    public async Task<Product?> GetByIdAsync(int id) 
        => await _context.Products.FindAsync(id);

    public async Task AddAsync(Product entity) 
        => await _context.Products.AddAsync(entity);

    public Task UpdateAsync(Product entity) 
    {
        _context.Products.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Product entity) 
    {
        _context.Products.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task SaveAsync() 
        => await _context.SaveChangesAsync();
}