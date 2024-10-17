using AastanApis.ErrorHandling;
using System.Text.Json.Serialization;

namespace AastanApis.Models;

public class ConsentInquiryResDto : ErrorResult
{
    [JsonPropertyName("recId")]
    public string? recId { get; init; }

    [JsonPropertyName("status")]
    public string? status { get; init; }
}