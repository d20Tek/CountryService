//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;

namespace D20Tek.CountryService.Controllers
{
    [ApiController]
    [Route("/api/v1/debug-log")]
    public class DebugLogController : OperationsControllerBase
    {
        public DebugLogController(ILogger<DebugLogController> logger, TelemetryClient telemetryClient)
            : base(logger, telemetryClient)
        {
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            return await this.ControllerOperationAsync<string>(nameof(Get), () =>
            {
                return Task.FromResult(new ActionResult<string>("Ok"));
            });
        }
    }
}
