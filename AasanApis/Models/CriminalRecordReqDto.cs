namespace AastanApis.Models;

public record CriminalRecordReqDto : BasePublicLogData
{
    public string? userNationalCode { get; init; }
    public string? nationalCode { get; init; }
    public string? organizationNationalCode { get; init; }
    public string? postName { get; init; }
    public string? organizationName { get; init; }
    public string? mobileNumber { get; init; }
    public string? registerCode { get; init; }
}