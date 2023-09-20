using System.Text.Json.Serialization;

namespace AasanApis.Models
{
    public sealed class MatchingEncryptReq
    {
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; }

        [JsonPropertyName("identificationNo")]
        public string IdentificationNo { get; set; }

        [JsonPropertyName("identificationType")]
        public int IdentificationType { get; set; }

        [JsonPropertyName("serviceType")]
        public int ServiceType { get; set; }
    }
}
