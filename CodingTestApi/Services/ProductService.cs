using CodingTestApi.Configuration;
using CodingTestApi.Models;
using Microsoft.Extensions.Options;

namespace CodingTestApi.Services;

/// <summary>
/// Product Service implementation - demonstrates Dependency Injection
/// Also demonstrates injecting IOptions for configuration
/// </summary>
public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;
    private readonly ApplicationSettings _settings;
    private static readonly List<Product> _products = new();
    private static int _nextId = 1;

    public ProductService(ILogger<ProductService> logger, IOptions<ApplicationSettings> settings)
    {
        _logger = logger;
        _settings = settings.Value;

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

    public Task<Product> CreateProductAsync(Product product)
    {
        product.Id = _nextId++;
        product.CreatedAt = DateTime.UtcNow;
        _products.Add(product);
        
        _logger.LogInformation("Created new product with ID: {ProductId}", product.Id);
        return Task.FromResult(product);
    }

    private void InitializeSampleData()
    {
        _products.AddRange(new[]
        {
            new Product
            {
                Id = _nextId++,
                Name = "Laptop",
                Price = 999.99m,
                Description = "High-performance laptop",
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            },
            new Product
            {
                Id = _nextId++,
                Name = "Mouse",
                Price = 29.99m,
                Description = "Wireless mouse",
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            },
            new Product
            {
                Id = _nextId++,
                Name = "Keyboard",
                Price = 79.99m,
                Description = "Mechanical keyboard",
                CreatedAt = DateTime.UtcNow.AddDays(-3)
            }
        });
    }
}
