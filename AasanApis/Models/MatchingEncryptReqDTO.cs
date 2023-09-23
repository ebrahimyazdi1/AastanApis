

namespace AasanApis.Models
{
    public record MatchingEncryptReqDTO :BasePublicLogData
    {
        public string RequestId { get; set; }

        public string IdentificationNo { get; set; }

        public int IdentificationType { get; set; }

        //[JsonPropertyName("serviceType")]
        //public int ServiceType { get; set; }
    }
}
