
using AasanApis.ErrorHandling;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AasanApis.Models
{
    public class RefreshTokenRes : ErrorResult
    {
        [JsonPropertyName("accessToken")]
        public string AccsessToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }


        [JsonPropertyName("scope")]
        public string Scope { get; set; }


        [JsonPropertyName("tokenType")]
        public string TokenType { get; set; }


        [JsonPropertyName("expiresIn")]
        public string ExpireTimesInSecond { get; set; }

        [NotMapped]
        public string RequestId { get; set; }
    }
}
