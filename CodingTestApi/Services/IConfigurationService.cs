namespace CodingTestApi.Services;

/// <summary>
/// Interface for configuration service - demonstrates Dependency Injection
/// </summary>
public interface IConfigurationService
{
    string GetApplicationInfo();
    Dictionary<string, string> GetAllSettings();
}
