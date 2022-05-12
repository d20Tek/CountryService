using D20Tek.CountryService.Controllers;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace D20Tek.CountryService.UnitTests.Mocks
{
    public class TestController : OperationsControllerBase
    {
        private static readonly TelemetryConfiguration _config = new TelemetryConfiguration(string.Empty);
        private static readonly Mock<ILogger<TestController>> _logger = new Mock<ILogger<TestController>>();
        private static readonly TelemetryClient _telemetryClient = new TelemetryClient(_config);

        [ExcludeFromCodeCoverage]
        public TestController(ILogger<TestController> logger, TelemetryClient telemetryClient)
            : base(logger, telemetryClient)
        {
        }

        public TestController()
            : base(_logger.Object, _telemetryClient)
        {
        }

        public async Task<ActionResult<string>> GetValidAsync()
        {
            return await this.ControllerOperationAsync<string>(
                nameof(GetValidAsync), () =>
                {
                    return Task.FromResult(new ActionResult<string>("Ok"));
                });
        }

        public async Task<ActionResult<string>> PutArgumentExceptionAsync()
        {
            return await this.ControllerOperationAsync<string>(
                nameof(PutArgumentExceptionAsync), () =>
                {
                    throw new ArgumentException();
                });
        }

        public async Task<ActionResult<string>> GetExceptionAsync()
        {
            return await this.ControllerOperationAsync<string>(
                nameof(GetExceptionAsync), () =>
                {
                    throw new NotImplementedException();
                });
        }

        public ActionResult<string> GetValid()
        {
            return this.ControllerOperation<string>(
                nameof(GetValid), () =>
                {
                    return new ActionResult<string>("Ok");
                });
        }

        public ActionResult<string> PutArgumentException()
        {
            return this.ControllerOperation<string>(
                nameof(PutArgumentException), () =>
                {
                    throw new ArgumentException();
                });
        }

        public ActionResult<string> GetException()
        {
            return this.ControllerOperation<string>(
                nameof(GetException), () =>
                {
                    throw new NotImplementedException();
                });
        }
    }
}
