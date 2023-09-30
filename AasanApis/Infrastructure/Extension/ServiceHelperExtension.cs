using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.OpenApi.Extensions;
using AasanApis.Models;
using AasanApis.ErrorHandling;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;

namespace AasanApis.Infrastructure.Extension
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

        public static void AddAastanCommonHeader(this HttpRequestMessage request, string refreshToken, AastanOptions options)
        {
            var authenticationParam =
                  Convert.ToBase64String(
                    Encoding.ASCII.GetBytes($"{options.UserName}:{options.Password}"));
            request.Headers.Add("Authorization", "Basic " + authenticationParam);
        }
        //public static FormUrlEncodedContent LoginFormUrlEncodedContent(AastanOptions options)
        //{
        //    var result = new Dictionary<string, string>
        //    {
        //        {"password", options.Password},
        //        {"username", options.UserName},
        //    };
        //    var formUrlEncodedContent = new FormUrlEncodedContent(result);
        //    return formUrlEncodedContent;
        //}
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
