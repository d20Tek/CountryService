//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Spectre.Console;
using Spectre.Console.Cli;

namespace D20Tek.CountryService.Cli.Commands
{
    internal class ConvertCsvToJsonCommand : AsyncCommand<BaseSettings>
    {
        public override async Task<int> ExecuteAsync(CommandContext context, BaseSettings settings)
        {
            AnsiConsole.WriteLine($"=> Executing command to convert CSV file to corresponding JSON.");
            return 0;
        }
    }
}
