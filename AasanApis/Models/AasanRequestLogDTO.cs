namespace AastanApis.Models
{
    public record AastanRequestLogDTO(string publicRequestId, string jsonRequest,
    string userId, string publicAppId, string serviceId);
}
