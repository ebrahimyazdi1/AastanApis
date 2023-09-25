using AasanApis.ErrorHandling;
using AasanApis.Exceptions;
using AasanApis.Filters;
using AasanApis.Infrastructure;
using AasanApis.Models;
using AasanApis.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AasanApis.Controllers
{
    [ApiExplorerSettings]
    [ApiVersion("1")]
    [Route("Shahkar/v1/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class AastanController : ControllerBase
    {
        private readonly ILogger<AastanController> _logger;
        private BaseLog _baseLog { get; }
        private IAastanService _astanService { get; }
        public AastanController(ILogger<AastanController> logger, BaseLog baseLog, IAastanService astanService)
        {
            _logger = logger;
            _baseLog = baseLog;
            _astanService = astanService;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(TokenResDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(TokenResDTO))]
        public async Task<ActionResult<TokenResDTO>> AastanGetToken(BasicDataReq basePublicLog)
        {
            var result = await _astanService.GetTokenAsync(basePublicLog);
            try
            {
                if (result.StatusCode != "OK")
                {
                    _logger.LogError($"{nameof(AastanGetToken)} not-success request - input \r\n response:{result.StatusCode}-{result.Content}");
                    return BadRequest(_baseLog.ApiResponeFailByCodeProvider<BasicDataReq>(result.Content, result.StatusCode, result.RequestId, basePublicLog?.PublicLogData?.PublicReqId));
                }
                return Ok(_baseLog.ApiResponseSuccessByCodeProvider<TokenResDTO>(result?.Content, result.StatusCode, result?.RequestId, basePublicLog?.PublicLogData?.PublicReqId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while {nameof(AastanGetToken)}");
                throw new RamzNegarException(ErrorCode.InternalError, $"Exception occurred while: {nameof(AastanGetToken)} => {ex.Message}");
            }

        }


        [AllowAnonymous]
        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RefreshTokenResDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RefreshTokenResDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(RefreshTokenResDTO))]
        public async Task<ActionResult<RefreshTokenResDTO>> AastanGetRefreshToken(RefreshTokenReqDTO refreshTokenReq)
        {
            var result = await _astanService.GetRefreshTokenAsync(refreshTokenReq);
            try
            {
                if (result.StatusCode != "OK")
                {
                    _logger.LogError($"{nameof(AastanGetRefreshToken)} not-success request - input \r\n response:{result.StatusCode}-{result.Content}");
                    return BadRequest(_baseLog.ApiResponeFailByCodeProvider<RefreshTokenReqDTO>(result.Content, result.StatusCode, result.RequestId, refreshTokenReq?.PublicLogData?.PublicReqId));
                }
                return Ok(_baseLog.ApiResponseSuccessByCodeProvider<RefreshTokenResDTO>(result?.Content, result.StatusCode, result?.RequestId, refreshTokenReq?.PublicLogData?.PublicReqId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while {nameof(AastanGetRefreshToken)}");
                throw new RamzNegarException(ErrorCode.InternalError, $"Exception occurred while: {nameof(AastanGetRefreshToken)} => {ex.Message}");
            }

        }


        [AllowAnonymous]
        [HttpPost("maching-encrypted")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MatchingEncryptResDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MatchingEncryptResDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MatchingEncryptResDTO))]
        public async Task<ActionResult<MatchingEncryptResDTO>> AastanMatchingEncrypted(MatchingEncryptReqDTO matchingEncryptReq)
        {
            var result = await _astanService.GetMatchingEncryptedAsync(matchingEncryptReq);
            try
            {
                if (result.StatusCode != "OK")
                {
                    _logger.LogError($"{nameof(AastanMatchingEncrypted)} not-success request - input \r\n response:{result.StatusCode}-{result.Content}");
                    return BadRequest(_baseLog.ApiResponeFailByCodeProvider<MatchingEncryptReqDTO>(result.Content, result.StatusCode, result.RequestId, matchingEncryptReq?.PublicLogData?.PublicReqId));
                }
                return Ok(_baseLog.ApiResponseSuccessByCodeProvider<MatchingEncryptResDTO>(result?.Content, result.StatusCode, result?.RequestId, matchingEncryptReq?.PublicLogData?.PublicReqId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while {nameof(AastanMatchingEncrypted)}");
                throw new RamzNegarException(ErrorCode.InternalError, $"Exception occurred while: {nameof(AastanMatchingEncrypted)} => {ex.Message}");
            }

        }
    }

}
