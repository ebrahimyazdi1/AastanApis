using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AastanApis.ErrorHandling;

namespace AastanApis.Models
{
    public class RefreshTokenRes : ErrorResult
    {
        [JsonPropertyName("access_token")]
        public string AccsessToken { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }


        [JsonPropertyName("scope")]
        public string Scope { get; set; }


        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }


        [JsonPropertyName("expires_in")]
        public long ExpireTimesInSecond { get; set; }

        [NotMapped]
        public string RequestId { get; set; }
    }
}
