﻿using System.Text.Json.Serialization;

namespace AastanApis.Models
{
    public class RefreshTokenReq
    {
        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; }

        [JsonPropertyName("token_refresh")]
        public string RefreshToken { get; set; }
    }
}
