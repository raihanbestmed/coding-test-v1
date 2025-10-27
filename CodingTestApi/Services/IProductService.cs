using CodingTestApi.Models;

namespace CodingTestApi.Services;

/// <summary>
/// Interface for Product Service - demonstrates Dependency Injection pattern
/// </summary>
public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
}
