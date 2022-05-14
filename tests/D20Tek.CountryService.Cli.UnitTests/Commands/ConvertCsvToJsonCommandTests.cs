using D20Tek.CountryService.Cli.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Spectre.Console.Cli;
using System.Threading.Tasks;

namespace D20Tek.CountryService.Cli.UnitTests.Commands
{
    [TestClass]
    public class ConvertCsvToJsonCommandTests
    {
        [TestMethod]
        public async Task ExecuteAsync_Default()
        {
            // arrange
            var context = this.CreateBasicContext("csv2json");
            var settings = new BaseSettings { Verbose = VerbosityLevel.low };
            var command = new ConvertCsvToJsonCommand();

            // act
            var result = await command.ExecuteAsync(context, settings);

            // assert
            Assert.AreEqual(0, result);
        }

        private CommandContext CreateBasicContext(string commandName)
        {
            var remaining = new Mock<IRemainingArguments>();
            return new CommandContext(remaining.Object, commandName, null);
        }
    }
}
