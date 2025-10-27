# BestMed Coding Test - .NET Core API Boilerplate

This is a boilerplate .NET Core Web API project for BestMed Coding Interview

## Project Structure

```
CodingTestApi/
├── Configuration/           # Configuration models
│   ├── ApplicationSettings.cs
│   └── DatabaseSettings.cs
├── Controllers/            # API Controllers
│   ├── ProductsController.cs
├── Models/                # Data models
│   └── Product.cs
├── Services/              # Service layer
│   ├── IProductService.cs
│   └── HardwareProductService.cs
├── appsettings.json       # Application configuration
└── Program.cs             # Application entry point
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

## API Endpoints

### Products API (Version 1)

- **GET** `/api/v1/products` - Get all products
- **GET** `/api/v1/products/{id}` - Get product by ID

## Testing the API

### Using curl

```bash
# Get all products (V1)
curl http://localhost:5000/api/v1/products

# Get product by ID
curl http://localhost:5000/api/v1/products/1
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

