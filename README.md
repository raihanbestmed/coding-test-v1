# BestMed Coding Test - .NET Core API Boilerplate

This is a boilerplate .NET Core Web API project demonstrating best practices for:
- **Dependency Injection (DI)**
- **Configuration Management** using `appsettings.json`
- **API Versioning**

## Project Structure

```
CodingTestApi/
├── Configuration/           # Configuration models
│   ├── ApplicationSettings.cs
│   └── DatabaseSettings.cs
├── Controllers/            # API Controllers
│   ├── V1/                # Version 1 endpoints
│   │   ├── ConfigurationController.cs
│   │   └── ProductsController.cs
│   └── V2/                # Version 2 endpoints
│       └── ProductsController.cs
├── Models/                # Data models
│   └── Product.cs
├── Services/              # Service layer
│   ├── IConfigurationService.cs
│   ├── ConfigurationService.cs
│   ├── IProductService.cs
│   └── ProductService.cs
├── appsettings.json       # Application configuration
└── Program.cs             # Application entry point
```

## Features Demonstrated

### 1. Dependency Injection (DI)

The project demonstrates DI with different service lifetimes:

- **Scoped Services** (`IProductService`): Created once per HTTP request
- **Singleton Services** (`IConfigurationService`): Created once for the application lifetime

Services are registered in `Program.cs`:
```csharp
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();
```

### 2. Configuration from appsettings.json

Configuration is loaded from `appsettings.json` using the Options pattern:

- `ApplicationSettings`: General application settings
- `DatabaseSettings`: Database-related configuration

Settings are bound in `Program.cs`:
```csharp
builder.Services.Configure<ApplicationSettings>(
    builder.Configuration.GetSection(ApplicationSettings.SectionName));
```

Services can then inject `IOptions<T>` to access configuration:
```csharp
public ProductService(ILogger<ProductService> logger, IOptions<ApplicationSettings> settings)
{
    _settings = settings.Value;
}
```

### 3. API Versioning

The API supports multiple versions using URL segment versioning (e.g., `/api/v1/products`, `/api/v2/products`):

- **V1**: Basic functionality
- **V2**: Enhanced responses with metadata

Versioning is configured in `Program.cs`:
```csharp
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});
```

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download) or later

### Running the Application

1. Navigate to the project directory:
   ```bash
   cd CodingTestApi
   ```

2. Build the project:
   ```bash
   dotnet build
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

The API will start and listen on `http://localhost:5000` (or `https://localhost:5001` for HTTPS).

## API Endpoints

### Products API (Version 1)

- **GET** `/api/v1/products` - Get all products
- **GET** `/api/v1/products/{id}` - Get product by ID
- **POST** `/api/v1/products` - Create a new product

### Products API (Version 2)

- **GET** `/api/v2/products` - Get all products (with enhanced response)
- **GET** `/api/v2/products/{id}` - Get product by ID (with enhanced response)
- **POST** `/api/v2/products` - Create a new product (with enhanced validation)

### Configuration API (Version 1)

- **GET** `/api/v1/configuration/info` - Get application information
- **GET** `/api/v1/configuration/settings` - Get all configuration settings

## Testing the API

### Using curl

```bash
# Get all products (V1)
curl http://localhost:5000/api/v1/products

# Get all products (V2 - enhanced response)
curl http://localhost:5000/api/v2/products

# Get product by ID
curl http://localhost:5000/api/v1/products/1

# Create a new product
curl -X POST http://localhost:5000/api/v1/products \
  -H "Content-Type: application/json" \
  -d '{"name":"Monitor","price":299.99,"description":"4K Monitor"}'

# Get application info (reads from appsettings.json)
curl http://localhost:5000/api/v1/configuration/info

# Get all settings (demonstrates configuration)
curl http://localhost:5000/api/v1/configuration/settings
```

### Example Responses

**V1 Products Response:**
```json
[
  {
    "id": 1,
    "name": "Laptop",
    "price": 999.99,
    "description": "High-performance laptop",
    "createdAt": "2025-10-17T19:39:48.831Z"
  }
]
```

**V2 Products Response (Enhanced):**
```json
{
  "version": "2.0",
  "count": 1,
  "data": [
    {
      "id": 1,
      "name": "Laptop",
      "price": 999.99,
      "description": "High-performance laptop",
      "createdAt": "2025-10-17T19:39:48.831Z"
    }
  ]
}
```

## Configuration

Edit `appsettings.json` to customize the application:

```json
{
  "ApplicationSettings": {
    "ApplicationName": "BestMed Coding Test API",
    "Version": "1.0.0",
    "MaxItemsPerPage": 50,
    "EnableDetailedErrors": true
  },
  "DatabaseSettings": {
    "ConnectionString": "Server=localhost;Database=CodingTestDb;",
    "CommandTimeout": 30,
    "EnableRetryOnFailure": true
  }
}
```

## Key Learning Points for Interviews

### Dependency Injection
- Understanding service lifetimes (Transient, Scoped, Singleton)
- Constructor injection pattern
- Interface-based programming

### Configuration
- Options pattern (`IOptions<T>`)
- Strongly-typed configuration
- Configuration binding from `appsettings.json`

### API Versioning
- URL segment versioning
- Multiple controller versions
- Version-specific responses

## Technologies Used

- **.NET 9.0**
- **ASP.NET Core Web API**
- **Asp.Versioning.Mvc** (v8.1.0)
- **Asp.Versioning.Mvc.ApiExplorer** (v8.1.0)

## License

This project is for educational purposes as part of the BestMed coding test.

