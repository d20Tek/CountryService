using D20Tek.CountryService.UnitTests.Mocks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace D20Tek.CountryService.UnitTests.Controllers
{
    [TestClass]
    public class OperationsControllerBaseTests
    {
        [TestMethod]
        public void Create()
        {
            // arrange

            // act
            var controller = new TestController();

            // assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task GetAsync()
        {
            // arrange
            var controller = new TestController();

            // act
            var result = await controller.GetValidAsync();

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Ok", result.Value);
        }

        [TestMethod]
        public async Task PutArgumentExceptionAsync()
        {
            // arrange
            var controller = new TestController();

            // act
            var result = await controller.PutArgumentExceptionAsync();

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status422UnprocessableEntity, HttpResultHelper.GetStatusCode(result));
        }

        [TestMethod]
        public async Task GetExceptionAsync()
        {
            // arrange
            var controller = new TestController();

            // act
            var result = await controller.GetExceptionAsync();

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, HttpResultHelper.GetStatusCode(result));
        }

        [TestMethod]
        public void Get()
        {
            // arrange
            var controller = new TestController();

            // act
            var result = controller.GetValid();

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Ok", result.Value);
        }

        [TestMethod]
        public void PutArgumentException()
        {
            // arrange
            var controller = new TestController();

            // act
            var result = controller.PutArgumentException();

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status422UnprocessableEntity, HttpResultHelper.GetStatusCode(result));
        }

        [TestMethod]
        public void GetException()
        {
            // arrange
            var controller = new TestController();

            // act
            var result = controller.GetException();

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, HttpResultHelper.GetStatusCode(result));
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [ExcludeFromCodeCoverage]
        public void Create_WithNullLogger()
        {
            // arrange

            // act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _ = new TestController(null, new TelemetryClient(new TelemetryConfiguration(string.Empty)));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [ExcludeFromCodeCoverage]
        public void Create_WithNullTelemetryClient()
        {
            // arrange

            // act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _ = new TestController(new Mock<ILogger<TestController>>().Object, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }
    }
}
