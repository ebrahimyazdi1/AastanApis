using AastanApis.ErrorHandling;
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
        public string ExpiresIn { get; set; }
    }
}
