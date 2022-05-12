using D20Tek.CountryService.Controllers;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace D20Tek.CountryService.UnitTests.Controllers
{
    [TestClass]
    public class DebugLogControllerTests
    {
        private static readonly TelemetryConfiguration _config = new TelemetryConfiguration(string.Empty);
        private readonly Mock<ILogger<DebugLogController>> _logger = new Mock<ILogger<DebugLogController>>();
        private readonly TelemetryClient _telemetryClient = new TelemetryClient(_config);

        [TestMethod]
        public async Task Get()
        {
            // arrange
            var controller = new DebugLogController(_logger.Object, _telemetryClient);

            // act
            var result = await controller.Get();

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Ok", result.Value);
        }
    }
}
