using AastanApis.Filters;
using AastanApis.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace AasanApis.Controllers
{
    [ApiExplorerSettings]
    [ApiVersion("v1")]
    [Route("Shahkar/v1/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class AastanController : ControllerBase
    {
        private readonly ILogger<AastanController> _logger;
        private  BaseLog _baseLog { get; }
        public AastanController(ILogger<AastanController> logger, BaseLog baseLog)
        {
            _logger = logger;
            _baseLog = baseLog;
        }

    }
}
