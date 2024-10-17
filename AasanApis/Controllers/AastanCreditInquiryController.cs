using AasanApis.Infrastructure;
using AasanApis.Models;
using AastanApis.ErrorHandling;
using AastanApis.Exceptions;
using AastanApis.Models;
using AastanApis.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AastanApis.Controllers;
public class AastanCreditInquiryController : Controller
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
    [HttpPost("token")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PgsbTokenRes))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PgsbTokenRes))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(PgsbTokenRes))]
    public async Task<ActionResult<PgsbTokenRes>> GetToken(BasePublicLogData basePublicLogData)
    {
        var result = await AastanService.GetPgsbTokenAsync(basePublicLogData);
        try
        {
            if (result.StatusCode is "OK")
                return Ok(_baseLog.ApiResponseSuccessByCodeProvider<PgsbTokenRes>(result.Content, result.StatusCode,
                    result.ReqLogId, result.RequestId));

            _logger.LogError($"{nameof(GetToken)} not-success request - input \r\n" +
                            $"response:{result.StatusCode}-{result.Content}");
            return BadRequest(_baseLog.ApiResponeFailByCodeProvider<BasePublicLogData>(result.Content,
                result.StatusCode, result.ReqLogId, result.RequestId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Exception occurred while {nameof(GetToken)}");
            throw new RamzNegarException(ErrorCode.InternalError, $"Exception occurred while:" +
                                                                  $" {nameof(GetToken)} => {ex.Message}");
        }
    }

    [AllowAnonymous]
    [HttpPost("Consent-Inquiry")]
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
}
