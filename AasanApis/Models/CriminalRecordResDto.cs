using AastanApis.ErrorHandling;
using System.Text.Json.Serialization;

namespace AastanApis.Models;

public class CriminalRecordResDto : ErrorResult
{
    [JsonPropertyName("checkRegisterCode")]
    public string? checkRegisterCode { get; init; }
    
    [JsonPropertyName("checkRegisterMessage")]
    public string? checkRegisterMessage { get; init; }

    [JsonPropertyName("criminalRecordResult")]
    public string? criminalRecordResult { get; init; }

    [JsonPropertyName("criminalRecordResultMessage")]
    public string? criminalRecordResultMessage { get; init; }

    [JsonPropertyName("result")]
    public string? result { get; init; }
}