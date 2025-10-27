using Asp.Versioning;
using CodingTestApi.Configuration;
using CodingTestApi.Services;

var builder = WebApplication.CreateBuilder(args);

// ===================================================================
// 1. CONFIGURATION - Reading from appsettings.json
// ===================================================================
// Bind configuration sections from appsettings.json to strongly-typed classes
ApplicationSettings defaultApplicationSettings = new ApplicationSettings
{
    ApplicationName = "CodingTestApi",
    Version = "1.0.0",
    MaxItemsPerPage = 50,
    EnableDetailedErrors = false
};

builder.Services.AddSingleton(c =>
    {
        var config = builder.Configuration.GetSection(ApplicationSettings.SectionName);
        var appSettings = config.Get<ApplicationSettings>();

        if (appSettings == null)
        {
            appSettings = defaultApplicationSettings;
        }

        return appSettings;
    });

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection(DatabaseSettings.SectionName));

// ===================================================================
// 2. DEPENDENCY INJECTION - Registering Services
// ===================================================================
// Register services with different lifetimes:

// Scoped: Created once per request
builder.Services.AddScoped<IProductService, HardwareProductService>();

// Add controllers
builder.Services.AddControllers();

// ===================================================================
// 3. API VERSIONING - Configure API Versioning
// ===================================================================
builder.Services.AddApiVersioning(options =>
{
    // Report API versions in response headers
    options.ReportApiVersions = true;

    // Set default API version if client doesn't specify
    options.DefaultApiVersion = new ApiVersion(1, 0);

    // Assume default version when not specified
    options.AssumeDefaultVersionWhenUnspecified = true;

    // Read API version from URL segment (e.g., /api/v1/products)
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddMvc()
.AddApiExplorer(options =>
{
    // Format version as "'v'major[.minor][-status]"
    options.GroupNameFormat = "'v'VVV";

    // Substitute version in URL
    options.SubstituteApiVersionInUrl = true;
});

// Add OpenAPI/Swagger support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

// ===================================================================
// HTTP Request Pipeline Configuration
// ===================================================================
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Map controllers (enables attribute routing)
app.MapControllers();

app.Run();
