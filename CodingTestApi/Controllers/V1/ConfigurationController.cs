using Asp.Versioning;
using CodingTestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodingTestApi.Controllers.V1;

/// <summary>
/// Configuration API - demonstrates reading from appsettings.json
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ConfigurationController : ControllerBase
{
    private readonly IConfigurationService _configurationService;
    private readonly ILogger<ConfigurationController> _logger;

    public ConfigurationController(
        IConfigurationService configurationService,
        ILogger<ConfigurationController> logger)
    {
        _configurationService = configurationService;
        _logger = logger;
    }

    /// <summary>
    /// Get application information from settings
    /// </summary>
    [HttpGet("info")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<string> GetInfo()
    {
        _logger.LogInformation("Getting application info");
        var info = _configurationService.GetApplicationInfo();
        return Ok(new { applicationInfo = info });
    }

    /// <summary>
    /// Get all settings from appsettings.json
    /// </summary>
    [HttpGet("settings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Dictionary<string, string>> GetSettings()
    {
        _logger.LogInformation("Getting all settings");
        var settings = _configurationService.GetAllSettings();
        return Ok(settings);
    }
}
