using CodingTestApi.Configuration;
using CodingTestApi.Models;
using Microsoft.Extensions.Options;

namespace CodingTestApi.Services;

/// <summary>
/// Product Service implementation - demonstrates Dependency Injection
/// Also demonstrates injecting IOptions for configuration
/// </summary>
public class SoftwareProductService : IProductService
{
    private readonly ILogger<SoftwareProductService> _logger;
    private readonly ApplicationSettings _settings;
    private static readonly List<Product> _products = new();
    private static int _nextId = 1;

    public SoftwareProductService(ILogger<SoftwareProductService> logger, ApplicationSettings settings)
    {
        _logger = logger;
        _settings = settings;

        // Initialize with sample data if empty
        if (!_products.Any())
        {
            InitializeSampleData();
        }
    }

    public Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        _logger.LogInformation("Getting all products. Max items per page: {MaxItems}", _settings.MaxItemsPerPage);
        return Task.FromResult<IEnumerable<Product>>(_products);
    }

    public Task<Product?> GetProductByIdAsync(int id)
    {
        _logger.LogInformation("Getting product with ID: {ProductId}", id);
        var product = _products.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(product);
    }

    private void InitializeSampleData()
    {
        _products.AddRange(new[]
        {
            new Product
            {
                Id = _nextId++,
                Name = "Chrome",
                Price = 999.99m,
                Description = "Chrome Browser",
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            },
            new Product
            {
                Id = _nextId++,
                Name = "FireFox",
                Price = 299.99m,
                Description = "FireFox Browser",
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            },
            new Product
            {
                Id = _nextId++,
                Name = "Opera",
                Price = 799.99m,
                Description = "Opera Browser",
                CreatedAt = DateTime.UtcNow.AddDays(-3)
            }
        });
    }
}
