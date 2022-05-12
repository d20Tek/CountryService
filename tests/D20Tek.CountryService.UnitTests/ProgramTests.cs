using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace D20Tek.CountryService.UnitTests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void ConfigureServices()
        {
            // arrange
            var services = this.CreateServiceCollectionMock();

            // act
            Program.ConfigureServices(services.Object);

            // assert
            services.Verify();
            Assert.AreEqual(168, services.Object.Count);
        }

        [TestMethod]
        public void CreateWebApplication_ProdEnv()
        {
            // arrange
            var args = Array.Empty<string>();

            // act
            var app = Program.CreateWebApplication(args);

            // assert
            Assert.IsNotNull(app);
        }

        [TestMethod]
        public void Configure_DevEnv()
        {
            // arrange
            var args = Array.Empty<string>();
            var hostEnv = CreateWebHostEnvironmentMock("Development");

            var builder = WebApplication.CreateBuilder(args);
            Program.ConfigureServices(builder.Services);
            var app = builder.Build();

            // act
            Program.Configure(app, hostEnv.Object, app as IEndpointRouteBuilder);

            // assert
            Assert.IsNotNull(app);
            hostEnv.Verify();
        }

        private Mock<ServiceCollection> CreateServiceCollectionMock()
        {
            var serviceCollectionMock = new Mock<ServiceCollection>()
            {
                CallBase = true,
            };

            return serviceCollectionMock;
        }

        private Mock<IWebHostEnvironment> CreateWebHostEnvironmentMock(string environmnet)
        {
            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();

            webHostEnvironmentMock.Setup(_ => _.EnvironmentName).Returns(environmnet);
            return webHostEnvironmentMock;
        }
    }
}