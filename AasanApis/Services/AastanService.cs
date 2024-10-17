using AastanApis.Data.Repositories;
using AasanApis.Models;
using AutoMapper;
using Microsoft.Extensions.Options;
using System.Text.Json;
using AastanApis.ErrorHandling;
using AastanApis.Exceptions;
using AastanApis.Models;
using Microsoft.OpenApi.Extensions;
using System.Net.Http;
using System.Text;

namespace AastanApis.Services
{
    public class AastanService : IAastanService
    {
        private IMapper _mapper;

        private readonly ILogger<AastanService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AastanOptions _astanOptions;

        public IWebHostEnvironment _webHostEnvironment { get; }
        private IAastanRepository _repository { get; set; }
        private IAastanClient _client { get; set; }

        IHttpContextAccessor httpContextAccessor { get; set; }

        public AastanService(IMapper mapper, ILogger<AastanService> logger, IConfiguration config,
           IAastanRepository repository, IAastanClient client, IWebHostEnvironment webHostEnvironment
           , IOptions<AastanOptions> astanOptions, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _client = client;
            _webHostEnvironment = webHostEnvironment;
            _astanOptions = astanOptions.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OutputModel> GetTokenAsync(BasePublicLogData basePublicLog)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetTokenAsync)} request sent - input is : \r\n {basePublicLog}");
                AastanRequestLogDTO astanRequest = new AastanRequestLogDTO(basePublicLog.PublicLogData?.PublicReqId, basePublicLog.ToString(),
                    basePublicLog.PublicLogData?.UserId, basePublicLog.PublicLogData?.PublicAppId, basePublicLog.PublicLogData?.ServiceId);
                string requestId = await _repository.InsertAastanRequestLog(astanRequest);
                var publicRequestId = _httpContextAccessor.HttpContext.Items["RequestId"] = basePublicLog.PublicLogData?.PublicReqId;
                var tokenResult = await _client.GetTokenAsync();
                if (tokenResult is null && !tokenResult.IsSuccess)
                {
                    return new OutputModel
                    {
                        Content = JsonSerializer.Serialize(tokenResult?.ResultMessage),
                        RequestId = publicRequestId?.ToString(),
                        StatusCode = tokenResult?.StatusCode,
                    };

                }
                await _repository.AddOrUpdateTokenAsync(tokenResult?.AccessToken);
                var tokenOutput = _mapper.Map<TokenResDTO>(tokenResult);
                ShahkarRequestsLogDTO shahkarEntity = new()
                {
                    AccessToken = tokenResult?.AccessToken,
                    ExpireTimeInSecond = tokenResult.ExpireTimesInSecond,
                    RefreshToken = tokenResult?.RefreshToken,
                    SafeServiceId = basePublicLog.PublicLogData.ServiceId ?? Guid.NewGuid().ToString(),
                    Scope = tokenResult?.Scope,
                    TokenType = tokenResult?.TokenType,
                    CreateDate = DateTime.Now,
                    ExpirationDateTime =
                            (DateTime.Now.AddSeconds(tokenResult.ExpireTimesInSecond))

                };
                await _repository.InsertShahkarRequestsLog(shahkarEntity);
                return new OutputModel
                {
                    Content = JsonSerializer.Serialize(tokenOutput) ?? JsonSerializer.Serialize(tokenResult?.ResultMessage),
                    RequestId = publicRequestId?.ToString(),
                    StatusCode = tokenResult?.StatusCode,
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Exception occurred while {nameof(GetTokenAsync)}");
                throw new RamzNegarException(ErrorCode.AastanApiError,
                    $"Exception occurred while: {nameof(GetTokenAsync)} => {ErrorCode.AastanApiError.GetDisplayName()}");
            }
        }
        public async Task<OutputModel> GetRefreshTokenAsync(RefreshTokenReqDTO refreshTokenReqDTO)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetRefreshTokenAsync)} request sent - input is : \r\n {refreshTokenReqDTO}");
                AastanRequestLogDTO astanRequest = new AastanRequestLogDTO(refreshTokenReqDTO.PublicLogData?.PublicReqId, refreshTokenReqDTO.ToString(),
                    refreshTokenReqDTO.PublicLogData?.UserId, refreshTokenReqDTO.PublicLogData?.PublicAppId, refreshTokenReqDTO.PublicLogData?.ServiceId);
                string requestId = await _repository.InsertAastanRequestLog(astanRequest);
                var refrehTokenReq = _mapper.Map<RefreshTokenReq>(refreshTokenReqDTO);
                var refreshToken = await GetAccessToken();
                refrehTokenReq.RefreshToken = refreshToken;
                var tokenResult = await _client.GetRefreshTokenAsync(refrehTokenReq);
                if (tokenResult is null || !tokenResult.IsSuccess)
                {
                    _logger.LogError($"the result of calling the RefreshTokenService is not ok {nameof(GetRefreshTokenAsync)}");
                    return new OutputModel
                    {
                        Content = JsonSerializer.Serialize(tokenResult),
                        RequestId = requestId,
                        StatusCode = tokenResult?.StatusCode,
                    };
                }
                //UpdateShahkarRequestsLogDTO updateRequest = new UpdateShahkarRequestsLogDTO
                //{
                //    RequestId = tokenResult.RequestId
                //};
                // _ = _repository.UpdateShahkarRequestsLog(updateRequest);

                //var tokenOutput = _mapper.Map<RefreshTokenResDTO>(tokenResult.ResultMessage);
                return new OutputModel
                {
                    Content = tokenResult.ResultMessage,
                    RequestId = requestId,
                    StatusCode = tokenResult?.StatusCode,
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Exception occurred while {nameof(GetRefreshTokenAsync)}");
                //return ServiceHelperExtension.GenerateErrorMethodResponse<TokenRes>(ErrorCode.NotFound);
                throw new RamzNegarException(ErrorCode.AastanApiError,
                    $"Exception occurred while: {nameof(GetRefreshTokenAsync)} => {ErrorCode.AastanApiError.GetDisplayName()}");
            }
        }
        public async Task<OutputModel> GetMatchingEncryptedAsync(MatchingEncryptReqDTO matchingEncryptReqDTO)
        {
            try
            {
                if (matchingEncryptReqDTO is null)
                {
                    //ServiceHelperExtension.GenerateApiErrorResponse<OutputModel>();
                }


                _logger.LogInformation($"{nameof(GetMatchingEncryptedAsync)} request sent - input is : \r\n {matchingEncryptReqDTO}");
                AastanRequestLogDTO astanRequest = new AastanRequestLogDTO(matchingEncryptReqDTO.PublicLogData?.PublicReqId, matchingEncryptReqDTO.ToString(),
                    matchingEncryptReqDTO.PublicLogData?.UserId, matchingEncryptReqDTO.PublicLogData?.PublicAppId, matchingEncryptReqDTO.PublicLogData?.ServiceId);
                string requestId = await _repository.InsertAastanRequestLog(astanRequest);
                var publicRequestId = _httpContextAccessor.HttpContext.Items["RequestId"] = matchingEncryptReqDTO.PublicLogData?.PublicReqId;
                var publicKey = JWESignManagement.Readkey(Path.Join(_webHostEnvironment.ContentRootPath,
                    "Certs", "Aastan-pubkey.pem")).Result;

                var data = GenerateData();
                //mobileNumber
                var tokenServiceNumber = JWESignManagement.GetEncryptedToken(matchingEncryptReqDTO.MobileNumber,
                    data.Iat, publicKey);

                //nationalCode
                var tokenIdentificationNo = JWESignManagement.GetEncryptedToken(matchingEncryptReqDTO.NationalCode,
                    data.Iat, publicKey);

                MatchingEncryptReq machingData = new MatchingEncryptReq
                {

                    IdentificationNo = tokenIdentificationNo,
                    RequestId = data.RequestId,
                    ServiceType = 2,
                    IdentificationType = 0,
                    ServiceNumber = tokenServiceNumber
                };


                var tokenResult = await _client.GetMatchingEncryptedAsync(machingData);
                if (tokenResult is null)
                {
                    _logger.LogError($"the result of calling the MatchingEncryptedService is not ok {nameof(GetMatchingEncryptedAsync)}");
                    return new OutputModel
                    {
                        Content = JsonSerializer.Serialize(tokenResult),
                        RequestId = publicRequestId?.ToString(),
                        StatusCode = tokenResult?.StatusCode,
                    };
                }
                //_ = _repository.UpdateShahkarRequestsLog(updateRequest);
                //to do I should update and some fields in shahkarEntity in the database
                var resResult = JsonSerializer.Deserialize<MatchingEncryptRes>(tokenResult.ResultMessage);
                var tokenOutput = _mapper.Map<MatchingEncryptResDTO>(tokenResult);
                if (resResult?.Result.Data.Response != 600)
                    tokenOutput.Matched = true;
                else
                    tokenOutput.Matched = false;

                return new OutputModel
                {
                    Content = JsonSerializer.Serialize(tokenOutput),
                    RequestId = publicRequestId?.ToString(),
                    StatusCode = tokenResult?.StatusCode,
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Exception occurred while {nameof(GetMatchingEncryptedAsync)}");
                throw new RamzNegarException(ErrorCode.AastanApiError,
                    $"Exception occurred while: {nameof(GetMatchingEncryptedAsync)} => {ErrorCode.AastanApiError.GetDisplayName()}");
            }
        }

        public async Task<OutputModel> GetPgsbTokenAsync(BasePublicLogData basePublicLogData)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetPgsbTokenAsync)} request sent - input is : \r\n {basePublicLogData}");
                var aastanRequest = new AastanRequestLogDTO(basePublicLogData.PublicLogData.PublicReqId, basePublicLogData.ToString(),
                    basePublicLogData.PublicLogData.UserId, basePublicLogData.PublicLogData.PublicAppId, basePublicLogData.PublicLogData.ServiceId);

                var requestId = await _repository.InsertAastanRequestLog(aastanRequest);
                var publicRequestId = _httpContextAccessor.HttpContext.Items["RequestId"] = basePublicLogData.PublicLogData.PublicReqId;

                var tokenResult = await _client.GetPgsbTokenAsync();
                
                if (tokenResult is null && !tokenResult.IsSuccess)
                {
                    return new OutputModel
                    {
                        Content = JsonSerializer.Serialize(tokenResult?.ResultMessage),
                        RequestId = publicRequestId.ToString(),
                        StatusCode = tokenResult?.StatusCode,
                        ReqLogId = requestId
                    };

                }
                await _repository.AddOrUpdateTokenAsync(tokenResult.access_token);
                var tokenOutput = _mapper.Map<TokenResDTO>(tokenResult);

                return new OutputModel
                {
                    Content = JsonSerializer.Serialize(tokenOutput),
                    RequestId = publicRequestId.ToString(),
                    StatusCode = tokenResult.StatusCode,
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Exception occurred while {nameof(GetPgsbTokenAsync)}");
                throw new RamzNegarException(ErrorCode.AastanApiError,
                    $"Exception occurred while: {nameof(GetPgsbTokenAsync)} => {ErrorCode.AastanApiError.GetDisplayName()}");
            }
        }

        public async Task<OutputModel> PostConsentInquiryAsync(ConsentInquiryReqDto request)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetTokenAsync)} request sent - input is : \r\n {request}");
                var aastanRequest = new AastanRequestLogDTO(request.PublicLogData.PublicReqId, request.ToString(),
                    request.PublicLogData.UserId, request.PublicLogData.PublicAppId, request.PublicLogData.ServiceId);

                var requestId = await _repository.InsertAastanRequestLog(aastanRequest);
                var publicRequestId = _httpContextAccessor.HttpContext.Items["RequestId"] = request.PublicLogData.PublicReqId;

                var tokenResult = await _client.PostConsentInquiryAsync(request);

                if (tokenResult is null && !tokenResult.IsSuccess)
                {
                    return new OutputModel
                    {
                        Content = JsonSerializer.Serialize(tokenResult?.ResultMessage),
                        RequestId = publicRequestId.ToString(),
                        StatusCode = tokenResult?.StatusCode,
                        ReqLogId = requestId
                    };

                }

                return new OutputModel
                {
                    StatusCode = tokenResult.StatusCode,
                    RequestId = tokenResult.recId,
                    Content = tokenResult.ResultMessage,
                    ReqLogId = requestId
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Exception occurred while {nameof(GetTokenAsync)}");
                throw new RamzNegarException(ErrorCode.AastanApiError,
                    $"Exception occurred while: {nameof(GetTokenAsync)} => {ErrorCode.AastanApiError.GetDisplayName()}");
            }
        }

        #region privateMethods
        private async Task<String> GetAccessToken()
        {
            var shahkarEntity = await _repository.FindAccessToken().ConfigureAwait(false);
            if (shahkarEntity is not null) return shahkarEntity.AccessToken;

            //Cause of nothing token find in shahkarLog have to call getToken service.
            var loginResponse = await _client.GetTokenAsync().ConfigureAwait(false);
            await _repository.UpdateShahkarRequestLogTokenAsync(loginResponse, shahkarEntity);
            return loginResponse.AccessToken;
        }

        //Generate the requestIt and Iat. requestId consists of 20 digits(ComponyProvider,dateString,6 Zero) 
        private MatchingModel GenerateData()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            string strDate = DateTime.Today.ToString("yyyyMMdd");
            string timeString = DateTime.Now.ToString("HHmmss");
            string requestId = _astanOptions.CompanyCode + strDate + timeString + "000000";
            return new MatchingModel { Iat = secondsSinceEpoch, RequestId = requestId };
        }
        #endregion

    }
}
