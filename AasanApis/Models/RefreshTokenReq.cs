using System.Text.Json.Serialization;

namespace AasanApis.Models
{
    public class RefreshTokenReq
    {
        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; }

        [JsonPropertyName("token_refresh")]
        public string RefreshToken { get; set; }
    }
}
