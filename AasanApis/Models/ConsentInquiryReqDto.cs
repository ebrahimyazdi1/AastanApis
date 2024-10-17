namespace AastanApis.Models;

public record ConsentInquiryReqDto : BasePublicLogData
{
    public string? PhoneNumber { get; init; }
    public string? NationalNumber { get; init; }
}