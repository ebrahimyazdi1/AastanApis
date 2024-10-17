﻿using System.Text.Json.Serialization;

namespace AastanApis.Models
{
    public class RefreshTokenResDTO
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        [JsonPropertyName("expires_in")]
        public long ExpireTimesInSecond{ get; set; }
    }
}
