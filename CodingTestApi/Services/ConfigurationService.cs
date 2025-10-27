using CodingTestApi.Configuration;
using Microsoft.Extensions.Options;

namespace CodingTestApi.Services;

/// <summary>
/// Configuration Service implementation - demonstrates reading from appsettings.json
/// and using IOptions pattern for configuration
/// </summary>
public class ConfigurationService : IConfigurationService
{
    private readonly ApplicationSettings _appSettings;
    private readonly DatabaseSettings _dbSettings;
    private readonly ILogger<ConfigurationService> _logger;

    public ConfigurationService(
        IOptions<ApplicationSettings> appSettings,
        IOptions<DatabaseSettings> dbSettings,
        ILogger<ConfigurationService> logger)
    {
        _appSettings = appSettings.Value;
        _dbSettings = dbSettings.Value;
        _logger = logger;
    }

    public string GetApplicationInfo()
    {
        _logger.LogInformation("Getting application information");
        return $"{_appSettings.ApplicationName} v{_appSettings.Version}";
    }

    public Dictionary<string, string> GetAllSettings()
    {
        _logger.LogInformation("Getting all configuration settings");
        
        return new Dictionary<string, string>
        {
            { "ApplicationName", _appSettings.ApplicationName },
            { "Version", _appSettings.Version },
            { "MaxItemsPerPage", _appSettings.MaxItemsPerPage.ToString() },
            { "EnableDetailedErrors", _appSettings.EnableDetailedErrors.ToString() },
            { "DatabaseTimeout", _dbSettings.CommandTimeout.ToString() },
            { "DatabaseRetryEnabled", _dbSettings.EnableRetryOnFailure.ToString() }
        };
    }
}
