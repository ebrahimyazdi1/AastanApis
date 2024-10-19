using AastanApis.ErrorHandling;
using System.Text.Json.Serialization;

namespace AastanApis.Models;

public class CriminalRecordRes : ErrorResult
{
    public int result { get; init; }
    public bool CheckRegisterCode { get; init; }
    public string CriminalRecordResultMessage { get; init; }
    public bool CriminalRecordResult { get; init; }
    public string CheckRegisterMessage { get; init; }
}