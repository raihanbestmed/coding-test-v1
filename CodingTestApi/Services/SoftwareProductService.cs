using CodingTestApi.Models;

namespace CodingTestApi.Services
{
    public class SoftwareProductService : IProductService
    {
        private static readonly List<Product> _products = new();
        private static int _nextId = 1;
        public SoftwareProductService()
        {
            // Initialize with sample data if empty
            if (!_products.Any())
            {
                InitializeSampleData();
            }
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        private void InitializeSampleData()
        {
            _products.AddRange(new[]
            {
                new Product
                {
                    Id = _nextId++,
                    Name = "Antivirus Software",
                    Price = 49.99m,
                    Description = "Protects your computer from viruses",
                    CreatedAt = DateTime.UtcNow.AddDays(-15)
                },
                new Product
                {
                    Id = _nextId++,
                    Name = "Photo Editing Software",
                    Price = 79.99m,
                    Description = "Advanced photo editing tools",
                    CreatedAt = DateTime.UtcNow.AddDays(-7)
                },
                new Product
                {
                    Id = _nextId++,
                    Name = "Project Management Software",
                    Price = 99.99m,
                    Description = "Tools for managing projects and tasks",
                    CreatedAt = DateTime.UtcNow.AddDays(-2)
                }   
        });
        }
    }
}
