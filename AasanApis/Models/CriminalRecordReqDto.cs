namespace AastanApis.Models;

public record CriminalRecordReqDto : BasePublicLogData
{
    public required string UserNationalCode { get; init; }
    public required string NationalCode { get; init; }
    public required string OrganizationNationalCode { get; init; }
    public required string PostName { get; init; }
    public required string OrganizationName { get; init; }
    public required string MobileNumber { get; init; }
    public required string RegisterCode { get; init; }
}
