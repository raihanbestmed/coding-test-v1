using Asp.Versioning;
using CodingTestApi.Models;
using CodingTestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodingTestApi.Controllers.V2;

/// <summary>
/// Products API Version 2.0
/// Demonstrates API Versioning - enhanced version with additional features
/// </summary>
[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    /// <summary>
    /// Get all products with enhanced response format
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAll()
    {
        _logger.LogInformation("V2: Getting all products with enhanced response");
        var products = await _productService.GetAllProductsAsync();
        
        // V2 returns enhanced response with metadata
        var response = new
        {
            version = "2.0",
            count = products.Count(),
            data = products
        };
        
        return Ok(response);
    }

    /// <summary>
    /// Get product by ID with enhanced response
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetById(int id)
    {
        _logger.LogInformation("V2: Getting product with ID: {ProductId}", id);
        var product = await _productService.GetProductByIdAsync(id);
        
        if (product == null)
        {
            return NotFound(new 
            { 
                version = "2.0",
                error = "NotFound",
                message = $"Product with ID {id} not found" 
            });
        }
        
        // V2 returns enhanced response with metadata
        var response = new
        {
            version = "2.0",
            data = product
        };
        
        return Ok(response);
    }

    /// <summary>
    /// Create a new product with enhanced validation
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create([FromBody] Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                version = "2.0",
                error = "ValidationError",
                details = ModelState
            });
        }

        // V2 has additional validation
        if (product.Price <= 0)
        {
            return BadRequest(new
            {
                version = "2.0",
                error = "ValidationError",
                message = "Price must be greater than zero"
            });
        }

        _logger.LogInformation("V2: Creating new product with enhanced validation");
        var createdProduct = await _productService.CreateProductAsync(product);
        
        var response = new
        {
            version = "2.0",
            message = "Product created successfully",
            data = createdProduct
        };
        
        return CreatedAtAction(
            nameof(GetById),
            new { id = createdProduct.Id, version = "2.0" },
            response);
    }
}
