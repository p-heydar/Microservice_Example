namespace Search.Infrastructure.Configurations;

public sealed class ElasticSearchConfiguration
{
    public string Host { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Fingerprint { get; set; }
}