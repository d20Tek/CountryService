﻿using D20Tek.CountryService.Cli.Injection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace D20Tek.CountryService.Cli.UnitTests.Injection
{
    [TestClass]
    public class TypeRegistrarTests
    {
        public interface ITestService { };

        public class TestService : ITestService { };

        [TestMethod]
        public void Register()
        {
            // arrange
            var services = new ServiceCollection();
            var registrar = new TypeRegistrar(services);

            // act
            registrar.Register(typeof(ITestService), typeof(TestService));

            // assert
            Assert.AreEqual(1, services.Count());
            Assert.IsTrue(services.Any(x => x.ServiceType == typeof(ITestService)));
            Assert.IsTrue(services.Any(x => x.ImplementationType == typeof(TestService)));
        }

        [TestMethod]
        public void RegisterInstance()
        {
            // arrange
            var services = new ServiceCollection();
            var registrar = new TypeRegistrar(services);
            var instance = new TestService();

            // act
            registrar.RegisterInstance(typeof(ITestService), instance);

            // assert
            Assert.AreEqual(1, services.Count());
            Assert.IsTrue(services.Any(x => x.ServiceType == typeof(ITestService)));
            Assert.IsFalse(services.Any(x => x.ImplementationType == typeof(TestService)));
            Assert.IsTrue(services.Any(x => x.ImplementationInstance == instance));
        }

        [TestMethod]
        public void RegisterLazy()
        {
            // arrange
            var services = new ServiceCollection();
            var registrar = new TypeRegistrar(services);

            // act
            registrar.RegisterLazy(typeof(ITestService), FactoryMethod);

            // assert
            Assert.AreEqual(1, services.Count());
            Assert.IsTrue(services.Any(x => x.ServiceType == typeof(ITestService)));
            Assert.IsFalse(services.Any(x => x.ImplementationType == typeof(TestService)));
            Assert.IsNotNull(services.First().ImplementationFactory);
        }

        [TestMethod]
        public void Build()
        {
            // arrange
            var services = new ServiceCollection();
            var registrar = new TypeRegistrar(services);

            registrar.Register(typeof(ITestService), typeof(TestService));

            // act
            var resolver = registrar.Build();

            // assert
            Assert.IsNotNull(resolver);
        }

        [ExcludeFromCodeCoverage]
        private TestService FactoryMethod() => new TestService();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [ExcludeFromCodeCoverage]
        public void RegisterLazy_WithNullFactory()
        {
            var services = new ServiceCollection();
            var registrar = new TypeRegistrar(services);

            // act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            registrar.RegisterLazy(typeof(ITestService), null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }
    }
}
