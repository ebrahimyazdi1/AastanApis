using AasanApis.ErrorHandling;
using AasanApis.Exceptions;
using AasanApis.Infrastructure;
using AasanApis.Infrastructure.Extension;
using AasanApis.Models;
using Microsoft.OpenApi.Extensions;
using System.Text.Json;

namespace AasanApis.Services
{
    public class AastanClient : IAastanClient
    {
        private readonly ILogger<AastanClient> _logger;
        
        private readonly HttpClient _httpClient;

        private readonly IAastanClient _astanClient;

        private readonly AastanOptions _astanOptions;
        
        private readonly BaseLog _baseLog;
        public AastanClient(ILogger<AastanClient> logger,
            HttpClient httpClient, IAastanClient astanClient, AastanOptions astanOptions)
        {
            _logger = logger;
            _httpClient = httpClient;
            _astanClient = astanClient;
            _astanOptions = astanOptions;
        }
        public async Task<MatchingEncryptRes> GetMatchingEncryptedAsync(MatchingEncryptReq matchingEncryptReq)
        {
            var response = await _baseLog.TransferSendAsync<MatchingEncryptReq, MatchingEncryptRes>
                (_astanOptions.RefreshTokenAddress, HttpMethod.Post, matchingEncryptReq);
            return response;
        }

        public async Task<RefreshTokenRes> GetRefreshTokenAsync(RefreshTokenReq refreshTokenReq)
        {
            //var tokenResult = await GetTokenAsync();
            var response = await _baseLog.TransferSendAsync<RefreshTokenReq, RefreshTokenRes>
                (_astanOptions.RefreshTokenAddress, HttpMethod.Post, refreshTokenReq);
            return response;
        }

        public async Task<TokenRes> GetTokenAsync()
        {
            try
            {
                var loginUri = new Uri(_astanOptions.TokenAddress, UriKind.RelativeOrAbsolute);
                var request = new HttpRequestMessage(HttpMethod.Post, loginUri);
                request.AddTokenHeader(_astanOptions);
                request.Content = ServiceHelperExtension.LoginFormUrlEncodedContent(_astanOptions);
                _logger.LogInformation($"{nameof(GetTokenAsync)} - request is: \r\n {JsonSerializer.Serialize(request)}");
                var response = await _httpClient.SendAsync(request)
                    .ConfigureAwait(false);
                var responseBodyJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
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
                    ExpireTimesInSeond = tokenOutput.ExpireTimesInSeond,
                    IsSuccess = response.IsSuccessStatusCode,
                    StatusCode = response.StatusCode.ToString(),
                    RefreshToken = tokenOutput.RefreshToken ?? "",
                    ResultMessage = responseBodyJson

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
