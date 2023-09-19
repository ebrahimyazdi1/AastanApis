namespace AasanApis.Models
{
    public record AasanResponseLogDTO(string publicRequestId, string jsonResponse,
           string asanHttpResponseCode, string asanRequestId, string asanResCode);
}
