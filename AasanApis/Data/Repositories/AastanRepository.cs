using AastanApis.Data.Entities;
using AastanApis.ErrorHandling;
using AastanApis.Exceptions;
using AastanApis.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Oracle.ManagedDataAccess.Client;

namespace AastanApis.Data.Repositories
{
    public class AastanRepository : IAastanRepository
    {
        public IConfiguration _configuration { get; set; }
        private readonly ILogger<AastanRepository> _logger;
        private readonly AastanDbContext _dbContext;
        private readonly IMapper _mapper;
        public AastanRepository(AastanDbContext dbContext, ILogger<AastanRepository> logger, IConfiguration configuration,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<string> InsertAastanRequestLog(AastanRequestLogDTO RequestLog)
        {
            string requestId = Guid.NewGuid().ToString();
            AastanReqLog ReqLog = new AastanReqLog
            {
                Id = requestId,
                LogDateTime = DateTime.Now,
                JsonReq = RequestLog.jsonRequest,
                UserId = RequestLog.userId,
                PublicAppId = RequestLog.publicAppId,
                ServiceId = RequestLog.serviceId,
                PublicReqId = RequestLog.publicRequestId
            };
            _dbContext.AastanReqLogs.Add(ReqLog);
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
                return requestId;
            }
            catch (OracleException ex)
            {
                _logger.LogError(ex, $"Exception occurred while {nameof(InsertAastanRequestLog)}");
                throw new ApplicationException($"Exception occurred while: {nameof(InsertAastanRequestLog)}  => {ex.Message}");
            }
        }

        public async Task<string> InsertAastanResponseLog(AastanResponseLogDTO ResponseLogDTO)
        {
            string responseId = Guid.NewGuid().ToString();
            AastanResLog resLog = new AastanResLog
            {
                Id = responseId,
                HTTPStatusCode = ResponseLogDTO.asanHttpResponseCode,
                JsonRes = ResponseLogDTO.jsonResponse,
                PublicReqId = ResponseLogDTO.publicRequestId,
                ReqLogId = ResponseLogDTO.asanRequestId,
                ResCode = ResponseLogDTO.asanResCode
            };
            _dbContext.AastanResLogs.Add(resLog);
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
                return responseId;
            }
            catch (OracleException ex)
            {
                _logger.LogError(ex, $"Exception occurred while {nameof(InsertAastanResponseLog)}");
                throw new ApplicationException($"Exception occurred while: {nameof(InsertAastanResponseLog)}  => {ex.Message}");
            }
        }

        public async Task AddOrUpdateTokenAsync(string? accessToken)
        {
            var accesTokenEntity = _dbContext.AccessTokens.SingleOrDefault(i => i.Id == "7");
            if (accesTokenEntity is null)
            {
                accesTokenEntity = new AccessTokenEntity();
                accesTokenEntity.Id = "7";
                accesTokenEntity.TokenName = "AastanToken";
                await _dbContext.AccessTokens.AddAsync(accesTokenEntity).ConfigureAwait(false);
            }
            accesTokenEntity.AccessToken = accessToken;
            accesTokenEntity.TokenDateTime = DateTime.Now;
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message,
                    $"{nameof(AddOrUpdateTokenAsync)} -> applyUpdateToken in AddOrUpdateTokenAsync couldn't update.");
                throw new RamzNegarException(ErrorCode.AastanTokenApiError,
                    $"Exception occurred while: {nameof(AddOrUpdateTokenAsync)}  => {ErrorCode.AastanTokenApiError.GetDisplayName()}");
            }


        }

        public async Task InsertShahkarRequestsLog(ShahkarRequestsLogDTO shahkarRequestsLogDTO)
        {

            var test = _dbContext.ChangeTracker.DebugView.LongView;

            var mappedEntity = _mapper.Map<ShahkarRequestsLogEntity>(shahkarRequestsLogDTO);
            var entity = _dbContext.ShahkarRequestsLog.Add(mappedEntity);
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Exception occurred while {nameof(InsertShahkarRequestsLog)}");
                throw new ApplicationException($"Exception occurred while: {nameof(InsertShahkarRequestsLog)}  => {ex.Message}");
            }
        }

        public async Task UpdateShahkarRequestsLog(UpdateShahkarRequestsLogDTO updateShahkarRequests)
        {
            var test = _dbContext.ChangeTracker.DebugView.LongView;
            ShahkarRequestsLogDTO updateShahkar = new ShahkarRequestsLogDTO()
            {
                RequestId = updateShahkarRequests?.RequestId,
            };
            var mappedEntity = _mapper.Map<ShahkarRequestsLogEntity>(updateShahkar);
            var entity = _dbContext.ShahkarRequestsLog.Add(mappedEntity);
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Exception occurred while {nameof(UpdateShahkarRequestsLog)}");
                throw new ApplicationException($"Exception occurred while: {nameof(UpdateShahkarRequestsLog)}  => {ex.Message}");
            }
        }

        public async Task<string> FindRefreshToken()
        {
            var refreshToken = await _dbContext.ShahkarRequestsLog
                .OrderByDescending(x => x.Id)
                .Select(x => x.RefreshToken)
                .FirstOrDefaultAsync();
            return refreshToken ?? string.Empty;
        }

        public async Task<string> FindRefreshTokenById(string accessToken)
        {
            var refreshToken = await _dbContext.ShahkarRequestsLog
              .Where(x => x.AccessToken == accessToken)
              .Select(x => x.RefreshToken)
              .FirstOrDefaultAsync();
            return refreshToken ?? string.Empty;
        }

        public async Task<ShahkarRequestsLogEntity> FindAccessToken()
        {
            var tokenEntity = await _dbContext.ShahkarRequestsLog
                .AsNoTracking().
                OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();
            var hasValidToken = tokenEntity != null && tokenEntity.ExpirationDateTime > DateTime.UtcNow;
            if (hasValidToken)
            {
                return tokenEntity;
            }
            return new ShahkarRequestsLogEntity();
        }
        public async Task<string> FindToken()
        {
            var tokenEntity = await _dbContext.ShahkarRequestsLog
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();
            var hasValidToken = tokenEntity != null && tokenEntity.ExpirationDateTime > DateTime.UtcNow;
            return hasValidToken ? tokenEntity.AccessToken : string.Empty;
        }

        public async Task<ShahkarRequestsLogEntity> UpdateShahkarRequestLogTokenAsync(TokenRes tokenRes,
            ShahkarRequestsLogEntity shahkarRequestsLogEntity)
        {
            if (shahkarRequestsLogEntity is null)
            {
                shahkarRequestsLogEntity = new ShahkarRequestsLogEntity();
                _dbContext.ShahkarRequestsLog.Add(shahkarRequestsLogEntity);
            }
            shahkarRequestsLogEntity.ExpirationDateTime = (DateTime.Now.AddSeconds(tokenRes.ExpireTimesInSecond));
            shahkarRequestsLogEntity.CreateDate = DateTime.Now;
            shahkarRequestsLogEntity.ErrorMessage = null;
            shahkarRequestsLogEntity.RefreshToken = tokenRes.RefreshToken;
            shahkarRequestsLogEntity.AccessToken = tokenRes.AccessToken;
            shahkarRequestsLogEntity.Scope = tokenRes.Scope;
            shahkarRequestsLogEntity.TokenType = tokenRes.TokenType;
            shahkarRequestsLogEntity.ExpireTimeInSecond = tokenRes.ExpireTimesInSecond;
            await _dbContext.SaveChangesAsync();
            return shahkarRequestsLogEntity;
        }

       
    }
}
