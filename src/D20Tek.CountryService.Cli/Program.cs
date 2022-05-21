//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.Spectre.Console.Extensions;

namespace D20Tek.CountryService.Cli
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            return await new CommandAppBuilder()
                             .WithDIContainer()
                             .WithStartup<Startup>()
                             .Build()
                             .RunAsync(args);
        }
    }
}
