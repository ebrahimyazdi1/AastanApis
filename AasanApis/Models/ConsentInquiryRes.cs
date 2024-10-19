using AastanApis.ErrorHandling;
using System.Text.Json.Serialization;

namespace AastanApis.Models;

public class ConsentInquiryRes : ErrorResult
{
    [JsonPropertyName("recId")]
    public long? RecId { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }
}