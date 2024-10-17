using AastanApis.Data.Repositories;
using AasanApis.Models;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using AastanApis.ErrorHandling;
using AastanApis.Exceptions;
using AastanApis.Infrastructure.Extension;
using AastanApis.Models;

namespace AasanApis.Infrastructure
{
    public class BaseLog
    {
        public IAastanRepository _repository { get; }
        private ILogger<BaseLog> _logger { get; }
        private AastanOptions _options { get; }

        private readonly HttpClient _httpClient;

        public BaseLog(IAastanRepository repository, ILogger<BaseLog> logger, IOptions<AastanOptions> options
                , HttpClient httpClient)
        {
            _repository = repository;
            _logger = logger;
            _options = options.Value;
            _httpClient = httpClient;
        }
        public T ApiResponseSuccessByCodeProvider<T>(string response, string statusCode, string RequestId, string publicReqId) where T : new()
        {
            _repository.InsertAastanResponseLog(new AastanResponseLogDTO(publicReqId, Convert.ToString(response), statusCode, RequestId, statusCode));
            var responseResult = JsonSerializer.Deserialize<T>(response);
            return responseResult;
        }
        public ErrorResult ApiResponeFailByCodeProvider<T>(string response, string statusCode, string RequestId, string publicReqId) where T : new()
        {
            var codeProvider = new ErrorCodesProvider();
            codeProvider = codeProvider.errorCodesResponseResult(statusCode.ToString());
            _repository.InsertAastanResponseLog(new AastanResponseLogDTO
                (publicReqId, Convert.ToString(response), codeProvider?.OutReponseCode.ToString(),
                         RequestId, codeProvider?.SafeReponseCode.ToString()));
            return ServiceHelperExtension.GenerateApiErrorResponse<ErrorResult>(codeProvider);
        }

        public ErrorResult ApiResponseFailByPSGBCodeProvider<T>(string response, string statusCode, string RequestId, string publicReqId) where T : new()
        {
            ErrorCodesProvider codeProvider = new ErrorCodesProvider();
            codeProvider = codeProvider.errorCodesResponseResult(statusCode.ToString());
            _repository.InsertAastanResponseLog(new AastanResponseLogDTO
               (publicReqId, Convert.ToString(response), codeProvider.OutReponseCode.ToString(),
                         RequestId, codeProvider.SafeReponseCode.ToString()));

            return ServiceHelperExtension.GenerateApiErrorResponse<ErrorResult>(codeProvider);
        }

            public async Task<TResponse> TransferSendAsync<TRequest, TResponse>(string uriString, HttpMethod method, TRequest request,
            FormUrlEncodedContent? encodedContent, [CallerMemberName] string callerMethodName = null)
        where TResponse : ErrorResult, new() where TRequest : class
        {
            {
                var delay = TimeSpan.FromSeconds(50);
                var cancellationToken = new CancellationTokenSource(delay).Token;
                var requestHttpMessage = new HttpRequestMessage(method, uriString);
                var accToken = await _repository.FindToken().ConfigureAwait(false);
                if (accToken is null || string.IsNullOrWhiteSpace(accToken))
                {
                    _logger.LogError($"An appropriate refreshToken not found -> {ErrorCode.NotFound.GetDisplayName()}");
                    throw new RamzNegarException(ErrorCode.TokenNotFound,
                                  ErrorCode.AastanApiError.GetDisplayName());
                }

                requestHttpMessage.AddAastanCommonHeader(accToken, _options);
                if (method == HttpMethod.Post && request != null)
                {
                    requestHttpMessage.Content =
                        new StringContent(
                            JsonSerializer.Serialize(request, ServiceHelperExtension.JsonSerializerOptions),
                    Encoding.UTF8, "application/json");
                }
                if (request is null && encodedContent !=null)
                {
                    requestHttpMessage.Content = encodedContent;
            
                }
              
                HttpResponseMessage httpResponseMessage;
                try
                {
                    httpResponseMessage = await _httpClient.SendAsync(requestHttpMessage, cancellationToken)
                        .ConfigureAwait(false);
                }
                catch (TaskCanceledException e)
                {
                    throw new RamzNegarException(ErrorCode.AastanApiError,
                        ErrorCode.AastanApiError.GetDisplayName());
                }
                catch (Exception e)
                {
                    _logger.LogError(e,
                        $"{callerMethodName} - request: '{request}' \r\n error message: {e.Message} ");
                    throw new RamzNegarException(ErrorCode.AastanApiError,
                                  ErrorCode.AastanApiError.GetDisplayName());
                }
                var responseContent = await (httpResponseMessage?.Content?.ReadAsStringAsync())
                    .ConfigureAwait(false);

                if (!httpResponseMessage.IsSuccessStatusCode)
                    return new TResponse()
                    {
                        IsSuccess = false,
                        ResultMessage = responseContent,
                        StatusCode = httpResponseMessage.StatusCode.ToString(),
                    };

                try
                {
                    var response = JsonSerializer.Deserialize<TResponse>(responseContent,
                        ServiceHelperExtension.JsonSerializerOptions);
                    response ??= new TResponse() { IsSuccess = true, StatusCode = httpResponseMessage.StatusCode.ToString() };
                    response.IsSuccess = true;
                    response.ResultMessage = responseContent;
                    response.StatusCode = httpResponseMessage.StatusCode.ToString();
                    return response;
                }
                catch (JsonException e)
                {
                    _logger.LogError(e,
                        $"{callerMethodName} - could not serialized: '{responseContent}' to: '{typeof(TResponse)}'");
                    throw new RamzNegarException(ErrorCode.InternalError, e.Message);
                }
                catch (Exception e)
                {
                    _logger.LogError(e,
                        $"{callerMethodName} - responseContent: '{responseContent}' \r\n error message: {e.Message} ");
                    throw new RamzNegarException(ErrorCode.InternalError, e.Message);
                }
            }
        }


    }
}
