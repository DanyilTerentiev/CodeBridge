namespace CodeBridge.Domain.Models;

public class RateLimitOptions
{
    public int PermitLimit { get; set; }

    public int Window { get; set; }

    public string PolicyName { get; set; } = null!;

    public int StatusCode { get; set; }
}