﻿using System.Text.Json.Serialization;
using System.Text.Json;
using AasanApis.Models;
using System.Text;
using AastanApis.ErrorHandling;
using Microsoft.OpenApi.Extensions;


namespace AastanApis.Infrastructure.Extension
{
    public static class ServiceHelperExtension
    {
        public static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReadCommentHandling = JsonCommentHandling.Skip,
            IgnoreNullValues = true
        };

        public static void AddAastanCommonHeader(this HttpRequestMessage request, string Token, AastanOptions options)
        {
            var basicAuthorizationParam =
            Convert.ToBase64String(
              Encoding.ASCII.GetBytes($"{options.RadioUserName}:{options.RadioPassword}"));
            request.Headers.Add("Accept", "application/json");
           // request.Headers.Add("Token", Token);
            request.Headers.Add("basicAuthorization", basicAuthorizationParam);
            var authenticationParam =
                  Convert.ToBase64String(
                    Encoding.ASCII.GetBytes($"{options.AstanUserName}:{options.AstanPassword}"));
            request.Headers.Add("Authorization", "Basic " + authenticationParam);
          
            //request.Headers.Add("basicAuthorization", basicAuthorizationParam);
            //request.Headers.Add("Token", Token);
           
        }
        
        public static T GenerateApiErrorResponse<T>(ErrorCodesProvider errorCode) where T : ErrorResult, new()
        {
            return new T
            {
                StatusCode = errorCode.OutReponseCode.ToString(),
                IsSuccess = false,
                ResultMessage = errorCode?.SafeReponseMesageDecription,

            };
        }

        public static T GenerateErrorMethodResponse<T>(ErrorCode errorCode) where T : ErrorResult, new()
        {
            return new T
            {
                StatusCode = errorCode.ToString(),
                IsSuccess = false,
                ResultMessage = errorCode.GetDisplayName()
            };
        }

    }
}
