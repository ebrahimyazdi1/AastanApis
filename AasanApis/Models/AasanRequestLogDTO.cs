namespace AasanApis.Models
{
    public record AasanRequestLogDTO(string publicRequestId, string jsonRequest,
    string userId, string publicAppId, string serviceId);
}
