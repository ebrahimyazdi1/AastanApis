namespace AastanApis.Models;

public record CriminalRecordResDto
{
    public int Result { get; init; }
    public bool CheckRegisterCode { get; init; }
    public string CriminalRecordResultMessage { get; init; }
    public bool CriminalRecordResult { get; init; }
    public string CheckRegisterMessage { get; init; }
}