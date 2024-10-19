using System.Text.Json.Serialization;
// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace AastanApis.Models;

public record ClientCriminalRecordReqDto
{
    [JsonPropertyName("UserNationalCode")]
    public string UserNationalCode { get; init; }

    [JsonPropertyName("NationalCode")]
    public string NationalCode { get; init; }

    [JsonPropertyName("OrganizationNationalCode")]
    public string OrganizationNationalCode { get; init; }

    [JsonPropertyName("PostName")]
    public string PostName { get; init; }

    [JsonPropertyName("OrganiationName")]
    public string OrganiationName { get; init; }

    [JsonPropertyName("MobileNumber")]
    public string MobileNumber { get; init; }

    [JsonPropertyName("RegisterCode")]
    public string RegisterCode { get; init; }
}