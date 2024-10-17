using AastanApis.ErrorHandling;
using System.Text.Json.Serialization;

namespace AastanApis.Models;

public class PgsbTokenRes : ErrorResult
{
    [JsonPropertyName("access_token")]
    public string? access_token { get; init; }
    
    [JsonPropertyName("token_type")]
    public string? token_type { get; init; }

    [JsonPropertyName("refresh_token")]
    public string? refresh_token { get; init; }

    [JsonPropertyName("expires_in")]
    public string? expires_in { get; init; }

    [JsonPropertyName("scope")]
    public string? scope { get; init; }
}