using AastanApis.Data.Repositories;
using AasanApis.Infrastructure;
using AasanApis.Models;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using System.Text;
using System.Text.Json;
using AastanApis.ErrorHandling;
using AastanApis.Exceptions;
using AastanApis.Infrastructure.Extension;
using AastanApis.Models;

namespace AastanApis.Services
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

        public async Task<PgsbTokenRes> GetPgsbTokenAsync()
        {
            try
            {
                var loginUri = new Uri(_astanOptions.PgsbTokenAddress, UriKind.RelativeOrAbsolute);
                var request = new HttpRequestMessage(HttpMethod.Post, loginUri);
                var accToken = await _repository.FindToken().ConfigureAwait(false);

                if (accToken is null || string.IsNullOrWhiteSpace(accToken))
                {
                    _logger.LogError($"An appropriate refreshToken not found -> {ErrorCode.NotFound.GetDisplayName()}");
                    throw new RamzNegarException(ErrorCode.TokenNotFound,
                        ErrorCode.AastanApiError.GetDisplayName());
                }
                request.AddAastanCommonHeader(accToken, _astanOptions);

                var response = await _httpClient.SendAsync(request)
                    .ConfigureAwait(false);

                var responseBodyJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"{nameof(GetPgsbTokenAsync)} -> the reason is {responseBodyJson}");
                    return ServiceHelperExtension.GenerateErrorMethodResponse<PgsbTokenRes>(ErrorCode.AastanApiError);
                }

                var tokenOutput =
                    JsonSerializer.Deserialize<PgsbTokenRes>(responseBodyJson,
                        ServiceHelperExtension.JsonSerializerOptions);

                if (string.IsNullOrWhiteSpace(tokenOutput?.access_token))
                {
                    _logger.LogError($"In the {nameof(GetPgsbTokenAsync)} access token is null-> {responseBodyJson}");
                    return ServiceHelperExtension.GenerateErrorMethodResponse<PgsbTokenRes>(ErrorCode.NotFound);
                }

                return new PgsbTokenRes
                {
                 access_token   = tokenOutput.access_token,
                 expires_in = tokenOutput.expires_in,
                 refresh_token = tokenOutput.refresh_token,
                 scope = tokenOutput.scope,
                 token_type = tokenOutput.token_type
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"Unable to get appropriateResponse: {nameof(GetPgsbTokenAsync)}, cause of {e.Message}");
                throw new RamzNegarException(ErrorCode.AastanApiError,
                    $"Exception occurred while: {nameof(GetPgsbTokenAsync)} => {ErrorCode.AastanApiError.GetDisplayName()}");
            }
        }

        public async Task<ConsentInquiryResDto> PostConsentInquiryAsync(ConsentInquiryReqDto consentInquiryRequest)
        {
            try
            {
                var url = new Uri(_astanOptions.PersonConsentInquiryAddress, UriKind.RelativeOrAbsolute);
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
                          JsonSerializer.Serialize(consentInquiryRequest, ServiceHelperExtension.JsonSerializerOptions),
                  Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request)
                .ConfigureAwait(false);

                var responseBodyJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"{nameof(PostConsentInquiryAsync)} -> the reason is {responseBodyJson}");
                    return ServiceHelperExtension.GenerateErrorMethodResponse<ConsentInquiryResDto>(ErrorCode.AastanApiError);
                }

                var responseDeserialize = JsonSerializer.Deserialize<ConsentInquiryResDto>(responseBodyJson,
                     ServiceHelperExtension.JsonSerializerOptions);
                responseDeserialize ??= new ConsentInquiryResDto { IsSuccess = true, StatusCode = response.StatusCode.ToString() };
                responseDeserialize.IsSuccess = true;
                responseDeserialize.ResultMessage = responseBodyJson;
                responseDeserialize.StatusCode = response.StatusCode.ToString();
                return responseDeserialize;

            }
            catch (Exception ex)
            {

                _logger.LogError($"Unable to get appropriateResponse: {nameof(PostConsentInquiryAsync)}, cause of {ex.Message}");
                throw new RamzNegarException(ErrorCode.AastanApiError,
                    $"Exception occurred while: {nameof(PostConsentInquiryAsync)} => {ErrorCode.AastanApiError.GetDisplayName()}");
            }
        }

        public async Task<CriminalRecordResDto> PostCriminalRecordAsync(CriminalRecordReqDto criminalRecordRequest)
        {
            try
            {
                var url = new Uri(_astanOptions.CriminalRecordAddress, UriKind.RelativeOrAbsolute);
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
                          JsonSerializer.Serialize(criminalRecordRequest, ServiceHelperExtension.JsonSerializerOptions),
                  Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request)
                .ConfigureAwait(false);

                var responseBodyJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"{nameof(PostConsentInquiryAsync)} -> the reason is {responseBodyJson}");
                    return ServiceHelperExtension.GenerateErrorMethodResponse<CriminalRecordResDto>(ErrorCode.AastanApiError);
                }

                var responseDeserialize = JsonSerializer.Deserialize<CriminalRecordResDto>(responseBodyJson,
                     ServiceHelperExtension.JsonSerializerOptions);
                responseDeserialize ??= new CriminalRecordResDto { IsSuccess = true, StatusCode = response.StatusCode.ToString() };
                responseDeserialize.IsSuccess = true;
                responseDeserialize.ResultMessage = responseBodyJson;
                responseDeserialize.StatusCode = response.StatusCode.ToString();
                return responseDeserialize;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to get appropriateResponse: {nameof(PostConsentInquiryAsync)}, cause of {ex.Message}");
                throw new RamzNegarException(ErrorCode.AastanApiError,
                    $"Exception occurred while: {nameof(PostConsentInquiryAsync)} => {ErrorCode.AastanApiError.GetDisplayName()}");
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
                return new TokenRes
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
