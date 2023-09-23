
using AasanApis.Data.Repositories;
using AasanApis.ErrorHandling;
using AasanApis.Exceptions;
using AasanApis.Models;
using AutoMapper;
using Microsoft.OpenApi.Extensions;
using System.Text.Json;

namespace AasanApis.Services
{
    public class AastanService : IAastanService
    {
        private IMapper _mapper;
        private readonly ILogger<AastanService> _logger;
        private IConfiguration _config { get; set; }
        private IBaseRepository _baseRepository { get; set; }
        private IAastanRepository _repository { get; set; }
        private IAastanClient _client { get; set; }

        public AastanService(IMapper mapper, ILogger<AastanService> logger, IConfiguration config,
            IBaseRepository baseRepository, IAastanRepository repository, IAastanClient client)
        {
            _mapper = mapper;
            _logger = logger;
            _config = config;
            _baseRepository = baseRepository;
            _repository = repository;
            _client = client;
        }

        public async Task<OutputModel> GetTokenAsync(BasePublicLogData basePublicLog)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetTokenAsync)} request sent - input is : \r\n {basePublicLog}");
                AastanRequestLogDTO aastanRequest=new AastanRequestLogDTO(basePublicLog.PublicLogData?.PublicReqId, basePublicLog.ToString(),
                    basePublicLog.PublicLogData?.UserId, basePublicLog.PublicLogData?.PublicAppId, basePublicLog.PublicLogData?.ServiceId);
                string requestId = await _repository.InsertAastanRequestLog(aastanRequest);
                var tokenResult = await _client.GetTokenAsync();
                if (tokenResult != null && tokenResult.IsSuccess)
                {
                    _ = await _baseRepository.AddOrUpdateTokenAsync(tokenResult?.AccessToken);
                }
                var tokenOutput = _mapper.Map<TokenResDTO>(tokenResult);
                return new OutputModel
                {
                    Content = JsonSerializer.Serialize(tokenOutput),
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
        public Task<OutputModel> GetMatchingEncryptedAsync(MatchingEncryptReqDTO matchingEncryptReq)
        {
            throw new NotImplementedException();
        }

        public Task<OutputModel> GetRefreshTokenAsync(RefreshTokenReqDTO refreshTokenReq)
        {
            throw new NotImplementedException();
        }


    }
}
