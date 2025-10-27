namespace CodingTestApi.Configuration;

/// <summary>
/// Application-wide settings loaded from appsettings.json
/// </summary>
public class ApplicationSettings
{
    public const string SectionName = "ApplicationSettings";

    public string ApplicationName { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public int MaxItemsPerPage { get; set; }
    public bool EnableDetailedErrors { get; set; }
}
