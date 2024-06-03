namespace MultiUserTaskManager.Api.Settings;

public class SqlServerSettings
{
    public string? Server { get; set; }
    public string? Database { get; set; }
    public string? User { get; set; }
    public string? Password { get; set; }
    public bool? TrustServerCertificate { get; set; }

    public string ConnectionString
    {
        get
        {
            return $"Server={Server};Database={Database};User Id={User};Password={Password};TrustServerCertificate={TrustServerCertificate};";
        }
    }
}
