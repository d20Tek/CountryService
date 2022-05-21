//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.CountryService.Cli.Commands;
using D20Tek.Spectre.Console.Extensions;
using Spectre.Console.Cli;

namespace D20Tek.CountryService.Cli
{
    internal class Startup : StartupBase
    {
        public override void ConfigureServices(ITypeRegistrar registrar)
        {
            // configure services here...

        }

        public override IConfigurator ConfigureCommands(IConfigurator config)
        {
            config.CaseSensitivity(CaseSensitivity.None);
            config.SetApplicationName("Country.Cli");
            config.ValidateExamples();

            config.AddCommand<ConvertCsvToJsonCommand>("csv2json")
                  .WithDescription("Loads CSV (comma separated value) file and converts it to the corresponding JSON file.")
                  .WithExample(new[] { "csv2json" });

            return config;
        }
    }
}
