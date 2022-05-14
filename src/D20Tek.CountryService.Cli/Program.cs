//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.CountryService.Cli.Commands;
using D20Tek.CountryService.Cli.Injection;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace D20Tek.CountryService.Cli
{
    class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var registrar = ConfigureServices(new ServiceCollection());

            var app = new CommandApp(registrar);
            app.Configure(config => ConfigureCommands(config));

            return await app.RunAsync(args);
        }

        internal static ITypeRegistrar ConfigureServices(IServiceCollection services)
        {
            // Create a type registrar and register any dependencies.
            // A type registrar is an adapter for a DI framework.

            // register services here...

            return new TypeRegistrar(services);
        }

        private static IConfigurator ConfigureCommands(IConfigurator config)
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
