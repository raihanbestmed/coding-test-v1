namespace CodingTestApi.Configuration;

/// <summary>
/// Database settings loaded from appsettings.json
/// </summary>
public class DatabaseSettings
{
    public const string SectionName = "DatabaseSettings";

    public string ConnectionString { get; set; } = string.Empty;
    public int CommandTimeout { get; set; }
    public bool EnableRetryOnFailure { get; set; }
}
