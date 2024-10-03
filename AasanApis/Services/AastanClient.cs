using AasanApis.Data.Repositories;
using AasanApis.ErrorHandling;
using AasanApis.Exceptions;
using AasanApis.Infrastructure;
using AasanApis.Infrastructure.Extension;
using AasanApis.Models;
using Azure;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace AasanApis.Services
{
    public class AastanClient : IAastanClient
    {
        private readonly ILogger<AastanClient> _logger;
        public IAastanRepository _repository { get; }
        private readonly HttpClient _httpClient;
        private readonly BaseLog _baseLog;
        private readonly AastanOptions _astanOptions;
        public AastanClient(ILogger<AastanClient> logger,
            HttpClient httpClient, IOptions<AastanOptions> astanOptions, BaseLog baseLog, IAastanRepository repository)
        {
            _logger = logger;
            _httpClient = httpClient;
            _astanOptions = astanOptions.Value;
            _baseLog = baseLog;
            _repository = repository;
        }


        public async Task<MatchingEncryptRes> GetMatchingEncryptedAsync(MatchingEncryptReq matchingEncryptReq)
        {

            try
            {
                var url = new Uri(_astanOptions.MachingServiceAddress, UriKind.RelativeOrAbsolute);
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                var accToken = await _repository.FindToken().ConfigureAwait(false);
                if (accToken is null || string.IsNullOrWhiteSpace(accToken))
                {
                    _logger.LogError($"An appropriate refreshToken not found -> {ErrorCode.NotFound.GetDisplayName()}");
                    throw new RamzNegarException(ErrorCode.TokenNotFound,
                                  ErrorCode.AastanApiError.GetDisplayName());
                }
                request.AddAastanCommonHeader(accToken, _astanOptions);
                request.Content =
                      new StringContent(
                          JsonSerializer.Serialize(matchingEncryptReq, ServiceHelperExtension.JsonSerializerOptions),
                  Encoding.UTF8, "application/json");
                var response = await _httpClient.SendAsync(request)
                .ConfigureAwait(false);
                var responseBodyJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"{nameof(GetTokenAsync)} -> the reason is {responseBodyJson}");
                    return ServiceHelperExtension.GenerateErrorMethodResponse<MatchingEncryptRes>(ErrorCode.AastanApiError);
                }
                var responseDeserialize = JsonSerializer.Deserialize<MatchingEncryptRes>(responseBodyJson,
                     ServiceHelperExtension.JsonSerializerOptions);
                responseDeserialize ??= new MatchingEncryptRes() { IsSuccess = true, StatusCode = response.StatusCode.ToString() };
                responseDeserialize.IsSuccess = true;
                responseDeserialize.ResultMessage = responseBodyJson;
                responseDeserialize.StatusCode = response.StatusCode.ToString();
                return responseDeserialize;

            }
            catch (Exception ex)
            {

                _logger.LogError($"Unable to get appropriateResponse: {nameof(GetMatchingEncryptedAsync)}, cause of {ex.Message}");
                throw new RamzNegarException(ErrorCode.AastanApiError,
                    $"Exception occurred while: {nameof(GetMatchingEncryptedAsync)} => {ErrorCode.AastanApiError.GetDisplayName()}");
            }
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
