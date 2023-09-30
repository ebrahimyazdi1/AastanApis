
using AasanApis.Data.Entities;
using AasanApis.Data.Repositories;
using AasanApis.ErrorHandling;
using AasanApis.Exceptions;
using AasanApis.Infrastructure.Extension;
using AasanApis.Models;
using AutoMapper;
using Microsoft.OpenApi.Extensions;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System.Text.Json;

namespace AasanApis.Services
{
    public class AastanService : IAastanService
    {
        private IMapper _mapper;
        private readonly ILogger<AastanService> _logger;
        private IConfiguration _config { get; set; }
        //private IBaseRepository _baseRepository { get; set; }
        private IAastanRepository _repository { get; set; }
        private IAastanClient _client { get; set; }

        IHttpContextAccessor httpContextAccessor { get; set; }

        public AastanService(IMapper mapper, ILogger<AastanService> logger, IConfiguration config,
           IAastanRepository repository, IAastanClient client)
        {
            _mapper = mapper;
            _logger = logger;
            _config = config;
            _repository = repository;
            _client = client;
        }

        public async Task<OutputModel> GetTokenAsync(BasePublicLogData basePublicLog)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetTokenAsync)} request sent - input is : \r\n {basePublicLog}");
                AastanRequestLogDTO astanRequest = new AastanRequestLogDTO(basePublicLog.PublicLogData?.PublicReqId, basePublicLog.ToString(),
                    basePublicLog.PublicLogData?.UserId, basePublicLog.PublicLogData?.PublicAppId, basePublicLog.PublicLogData?.ServiceId);
                string requestId = await _repository.InsertAastanRequestLog(astanRequest);
                var tokenResult = await _client.GetTokenAsync();
                if (tokenResult is null && !tokenResult.IsSuccess)
                {
                    return new OutputModel
                    {
                        Content = JsonSerializer.Serialize(tokenResult?.ResultMessage),
                        RequestId = requestId,
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
                    CreateDate = DateTime.UtcNow,
                    ExpirationDateTime =
                            (DateTime.Now.AddSeconds(tokenResult.ExpireTimesInSecond))

                };
                await _repository.InsertShahkarRequestsLog(shahkarEntity);
                return new OutputModel
                {
                    Content = JsonSerializer.Serialize(tokenOutput) ?? JsonSerializer.Serialize(tokenResult?.ResultMessage),
                    RequestId = requestId,
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
                var refreshToken = await GetRefreshToken();
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
                UpdateShahkarRequestsLogDTO updateRequest = new UpdateShahkarRequestsLogDTO
                {
                    RequestId = tokenResult.RequestId
                };
                _ = _repository.UpdateShahkarRequestsLog(updateRequest);

                var tokenOutput = _mapper.Map<RefreshTokenResDTO>(tokenResult);
                return new OutputModel
                {
                    Content = JsonSerializer.Serialize(tokenOutput),
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
                _logger.LogInformation($"{nameof(GetMatchingEncryptedAsync)} request sent - input is : \r\n {matchingEncryptReqDTO}");
                AastanRequestLogDTO astanRequest = new AastanRequestLogDTO(matchingEncryptReqDTO.PublicLogData?.PublicReqId, matchingEncryptReqDTO.ToString(),
                    matchingEncryptReqDTO.PublicLogData?.UserId, matchingEncryptReqDTO.PublicLogData?.PublicAppId, matchingEncryptReqDTO.PublicLogData?.ServiceId);
                string requestId = await _repository.InsertAastanRequestLog(astanRequest);
                var mappedMatching = _mapper.Map<MatchingEncryptReq>(matchingEncryptReqDTO);
                var tokenResult = await _client.GetMatchingEncryptedAsync(mappedMatching);
                if (tokenResult is null || !tokenResult.IsSuccess)
                {
                    _logger.LogError($"the result of calling the MatchingEncryptedService is not ok {nameof(GetMatchingEncryptedAsync)}");
                    return new OutputModel
                    {
                        Content = JsonSerializer.Serialize(tokenResult),
                        RequestId = requestId,
                        StatusCode = tokenResult?.StatusCode,
                    };
                }
                //_ = _repository.UpdateShahkarRequestsLog(updateRequest);
                //to do I should update and some fields in shahkarEntity in the database
                var tokenOutput = _mapper.Map<MatchingEncryptResDTO>(tokenResult);

                return new OutputModel
                {
                    Content = JsonSerializer.Serialize(tokenOutput),
                    RequestId = requestId,
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


        private async Task<String> GetRefreshToken()
        {
            var shahkarEntity = await _repository.FindAccessToken().ConfigureAwait(false);
            if (shahkarEntity is not null) return shahkarEntity.AccessToken;

            //Cause of nothing token find in shahkarLog have to call getToken service.
            var loginResponse = await _client.GetTokenAsync().ConfigureAwait(false);
            await _repository.UpdateShahkarRequestLogTokenAsync(loginResponse, shahkarEntity);
            return loginResponse.RefreshToken;
        }
    }
}
