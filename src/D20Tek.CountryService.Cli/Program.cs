//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.CountryService.Cli.Commands;
using Spectre.Console.Cli;

namespace Activity.Metadata.Cli
{
    class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var app = new CommandApp();
            app.Configure(config =>
            {
                config.CaseSensitivity(CaseSensitivity.None);
                config.SetApplicationName("Country.Cli");
                config.ValidateExamples();

                config.AddCommand<ConvertCsvToJsonCommand>("csv2json")
                    .WithDescription("Loads a CSV (comma separated value) file and converts it to the corresponding JSON file.")
                    .WithExample(new[] { "csv2json" });
            });

            return await app.RunAsync(args);
        }
    }
}
