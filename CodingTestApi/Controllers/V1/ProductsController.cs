using Asp.Versioning;
using CodingTestApi.Models;
using CodingTestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodingTestApi.Controllers.V1;

/// <summary>
/// Products API Version 1.0
/// Demonstrates API Versioning and Dependency Injection
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    // Constructor injection - demonstrates Dependency Injection
    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    /// <summary>
    /// Get all products
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        _logger.LogInformation("V1: Getting all products");
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    /// <summary>
    /// Get product by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        _logger.LogInformation("V1: Getting product with ID: {ProductId}", id);
        var product = await _productService.GetProductByIdAsync(id);
        
        if (product == null)
        {
            return NotFound(new { message = $"Product with ID {id} not found" });
        }
        
        return Ok(product);
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> Create([FromBody] Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _logger.LogInformation("V1: Creating new product");
        var createdProduct = await _productService.CreateProductAsync(product);
        
        return CreatedAtAction(
            nameof(GetById),
            new { id = createdProduct.Id, version = "1.0" },
            createdProduct);
    }
}
