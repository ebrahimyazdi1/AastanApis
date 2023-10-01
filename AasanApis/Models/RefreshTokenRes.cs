
using AasanApis.ErrorHandling;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AasanApis.Models
{
    public class RefreshTokenRes : ErrorResult
    {
        [JsonPropertyName("access_Token")]
        public string AccsessToken { get; set; }

        [JsonPropertyName("refresh_Token")]
        public string RefreshToken { get; set; }


        [JsonPropertyName("scope")]
        public string Scope { get; set; }


        [JsonPropertyName("token_Type")]
        public string TokenType { get; set; }


        [JsonPropertyName("expires_In")]
        public long ExpireTimesInSecond { get; set; }

        [NotMapped]
        public string RequestId { get; set; }
    }
}
