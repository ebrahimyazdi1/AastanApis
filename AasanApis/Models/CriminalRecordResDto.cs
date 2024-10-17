namespace AastanApis.Models;

public record CriminalRecordResDto
{
    public string?  CheckRegisterCode { get; init; }
    public string? CheckRegisterMessage { get; init; }
    public string? CriminalRecordResult { get; init; } 
    public string? CriminalRecordResultMessage { get; init; } 
    public string? Result { get; init; }
}