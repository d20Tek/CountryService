using D20Tek.CountryService.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace D20Tek.CountryService.UnitTests.Models
{
    [TestClass]
    public class CountryCodeTests
    {
        [TestMethod]
        public void Create()
        {
            // arrange

            // act
            var code = new CountryCode
            {
                CountryId = 101,
                Name = "Test",
                FullName = "Testlandia",
                Alpha2Code = "TS",
                Alpha3Code = "TST",
                Sovereignty = 0,
                TopLevelDomain = ".ts",
            };

            // assert
            Assert.IsNotNull(code);
            Assert.AreEqual(101, code.CountryId);
            Assert.AreEqual("Test", code.Name);  
            Assert.AreEqual("Testlandia", code.FullName);  
            Assert.AreEqual("TS", code.Alpha2Code);
            Assert.AreEqual("TST", code.Alpha3Code);
            Assert.AreEqual(0, code.Sovereignty);
            Assert.AreEqual(".ts", code.TopLevelDomain);
        }
    }
}
