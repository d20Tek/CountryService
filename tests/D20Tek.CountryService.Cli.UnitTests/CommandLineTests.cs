using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spectre.Console;
using System.Threading.Tasks;

namespace D20Tek.CountryService.Cli.UnitTests
{
    [TestClass]
    public class CommandLineTests
    {
        [TestMethod]
        public async Task ConfiguredCommands()
        {
            // arrange
            var args = new string[] { "-h" };
            AnsiConsole.Record();

            // act
            var result = await Program.Main(args);

            // assert
            Assert.AreEqual(0, result);
            var text = AnsiConsole.ExportText();
            StringAssert.Contains(text, "Country.Cli");
            StringAssert.Contains(text, "csv2json");
        }
    }
}