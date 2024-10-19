using AasanApis.Filters;
using AasanApis.Infrastructure;
using AasanApis.Models;
using AastanApis.ErrorHandling;
using AastanApis.Exceptions;
using AastanApis.Models;
using AastanApis.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AastanApis.Controllers;

[ApiExplorerSettings]
[ApiVersion("1")]
[Route("PSGBShahkar/v1/[controller]")]
[ApiController]
[ApiResultFilter]
public class AastanCreditInquiryController : ControllerBase
{

    private readonly ILogger<AastanCreditInquiryController> _logger;
    private BaseLog _baseLog { get; }
    private IAastanService AastanService { get; }
    public AastanCreditInquiryController(ILogger<AastanCreditInquiryController> logger, BaseLog baseLog, IAastanService aastanService)
    {
        _logger = logger;
        _baseLog = baseLog;
        AastanService = aastanService;
    }

    [AllowAnonymous]
    [HttpPost("psgb-token")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PgsbTokenResDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PgsbTokenResDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(PgsbTokenResDto))]
    public async Task<ActionResult<PgsbTokenResDto>> GetPsgbToken(BasicDataReq basePublicLogData)
    {
        var result = await AastanService.GetPgsbTokenAsync(basePublicLogData);
        try
        {
            if (result.StatusCode is "OK")
                return Ok(_baseLog.ApiResponseSuccessByCodeProvider<PgsbTokenResDto>(result.Content, result.StatusCode,
                    result.ReqLogId, result.RequestId));

            _logger.LogError($"{nameof(GetPsgbToken)} not-success request - input \r\n" +
                            $"response:{result.StatusCode}-{result.Content}");
            return BadRequest(_baseLog.ApiResponeFailByCodeProvider<BasePublicLogData>(result.Content,
                result.StatusCode, result.ReqLogId, result.RequestId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Exception occurred while {nameof(GetPsgbToken)}");
            throw new RamzNegarException(ErrorCode.InternalError, $"Exception occurred while:" +
                                                                  $" {nameof(GetPsgbToken)} => {ex.Message}");
        }
    }

    [AllowAnonymous]
    [HttpPost("consent-inquiry")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConsentInquiryResDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ConsentInquiryResDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ConsentInquiryResDto))]
    public async Task<ActionResult<ConsentInquiryResDto>> PostConsentInquiry(ConsentInquiryReqDto request)
    {
        var result = await AastanService.PostConsentInquiryAsync(request);
        try
        {
            if (result.StatusCode is "OK")
                return Ok(_baseLog.ApiResponseSuccessByCodeProvider<ConsentInquiryResDto>(result.Content, result.StatusCode,
                    result.ReqLogId, result.RequestId));

            _logger.LogError($"{nameof(PostConsentInquiry)} not-success request - input \r\n" +
                             $"response:{result.StatusCode}-{result.Content}");
            return BadRequest(_baseLog.ApiResponeFailByCodeProvider<ConsentInquiryReqDto>(result.Content,
                result.StatusCode, result.ReqLogId, result.RequestId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Exception occurred while {nameof(PostConsentInquiry)}");
            throw new RamzNegarException(ErrorCode.InternalError, $"Exception occurred while:" +
                                                                  $" {nameof(PostConsentInquiry)} => {ex.Message}");
        }
    }

    [AllowAnonymous]
    [HttpPost("criminal-record")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CriminalRecordResDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CriminalRecordResDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CriminalRecordResDto))]
    public async Task<ActionResult<CriminalRecordResDto>> PostCriminalRecord(CriminalRecordReqDto request)
    {
        var result = await AastanService.PostCriminalRecordAsync(request);
        try
        {
            if (result.StatusCode is "OK")
                return Ok(_baseLog.ApiResponseSuccessByCodeProvider<CriminalRecordResDto>(result.Content, result.StatusCode,
                    result.ReqLogId, result.RequestId));

            _logger.LogError($"{nameof(PostCriminalRecord)} not-success request - input \r\n" +
                             $"response:{result.StatusCode}-{result.Content}");
            return BadRequest(_baseLog.ApiResponseFailByPSGBCodeProvider<CriminalRecordResDto>(result.Content,
                result.StatusCode, result.ReqLogId, result.RequestId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Exception occurred while {nameof(PostCriminalRecord)}");
            throw new RamzNegarException(ErrorCode.InternalError, $"Exception occurred while:" +
                                                                  $" {nameof(PostCriminalRecord)} => {ex.Message}");
        }
    }
}
