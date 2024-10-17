namespace AastanApis.Models
{
    public record AastanResponseLogDTO(string publicRequestId, string jsonResponse,
           string asanHttpResponseCode, string asanRequestId, string asanResCode);
}
