namespace AastanApis.Models
{
    public record MatchingEncryptReqDTO : BasePublicLogData
    {
        public string NationalCode { get; set; }
        public string MobileNumber { get; set; }
    }
}
