using AasanApis.ErrorHandling;
using AasanApis.Exceptions;
using AasanApis.Infrastructure;
using AasanApis.Infrastructure.Extension;
using AasanApis.Models;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using System.Text.Json;

namespace AasanApis.Services
{
    public class AastanClient : IAastanClient
    {
        private readonly ILogger<AastanClient> _logger;

        private readonly HttpClient _httpClient;
        private readonly BaseLog _baseLog;
        private readonly AastanOptions _astanOptions;
        public AastanClient(ILogger<AastanClient> logger,
            HttpClient httpClient, IOptions<AastanOptions> astanOptions, BaseLog baseLog)
        {
            _logger = logger;
            _httpClient = httpClient;
            _astanOptions = astanOptions.Value;
            _baseLog = baseLog;
        }

       
        public async Task<MatchingEncryptRes> GetMatchingEncryptedAsync(MatchingEncryptReq matchingEncryptReq)
        {

            var str = JsonSerializer.Serialize(matchingEncryptReq);
            var response = await _baseLog.TransferSendAsync<MatchingEncryptReq, MatchingEncryptRes>
                (_astanOptions.MachingServiceAddress, HttpMethod.Post, matchingEncryptReq, null);
            return response;
        }

        public async Task<RefreshTokenRes> GetRefreshTokenAsync(RefreshTokenReq refreshTokenReq)
        {
            var result = new Dictionary<string, string>
            {
                {"grant_type", _astanOptions.GrantType},
                {"refresh_token", refreshTokenReq.RefreshToken},
            };
            var formUrlEncodedContent = new FormUrlEncodedContent(result);
            var response = await _baseLog.TransferSendAsync<RefreshTokenReq, RefreshTokenRes>
                (_astanOptions.RefreshTokenAddress, HttpMethod.Post, null, formUrlEncodedContent);
            return response;
        }

        public async Task<TokenRes> GetTokenAsync()
        {
            try
            {
                var loginUri = new Uri(_astanOptions.TokenAddress, UriKind.RelativeOrAbsolute);
                var request = new HttpRequestMessage(HttpMethod.Post, loginUri);
                _logger.LogInformation($"{nameof(GetTokenAsync)} - request is: \r\n {JsonSerializer.Serialize(request)}");
                var response = await _httpClient.SendAsync(request)
                    .ConfigureAwait(false);
                var responseBodyJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"{nameof(GetTokenAsync)} -> the reason is {responseBodyJson}");
                    return ServiceHelperExtension.GenerateErrorMethodResponse<TokenRes>(ErrorCode.AastanApiError);
                }
                var tokenOutput =
                    JsonSerializer.Deserialize<TokenRes>(responseBodyJson,
                        ServiceHelperExtension.JsonSerializerOptions);
                if (string.IsNullOrWhiteSpace(tokenOutput?.AccessToken))
                {
                    _logger.LogError($"In the {nameof(GetTokenAsync)} access token is null-> {responseBodyJson}");
                    return ServiceHelperExtension.GenerateErrorMethodResponse<TokenRes>(ErrorCode.NotFound);
                }
                return new TokenRes()
                {
                    AccessToken = tokenOutput?.AccessToken ?? "",
                    ExpireTimesInSecond = tokenOutput.ExpireTimesInSecond,
                    IsSuccess = response.IsSuccessStatusCode,
                    StatusCode = response.StatusCode.ToString(),
                    RefreshToken = tokenOutput.RefreshToken ?? "",
                    ResultMessage = responseBodyJson,
                    Scope = tokenOutput.Scope,
                    TokenType = tokenOutput.TokenType

                };
            }
            catch (Exception e)
            {
                _logger.LogError($"Unable to get appropriateResponse: {nameof(GetTokenAsync)}, cause of {e.Message}");
                throw new RamzNegarException(ErrorCode.AastanApiError,
                    $"Exception occurred while: {nameof(GetTokenAsync)} => {ErrorCode.AastanApiError.GetDisplayName()}");
            }
        }
    }
}
