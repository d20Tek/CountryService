//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Spectre.Console.Cli;
using System.ComponentModel;

namespace D20Tek.CountryService.Cli.Commands
{
    internal class BaseSettings : CommandSettings
    {
        [CommandOption("-v|--Verbose <VERBOSE-LEVEL>")]
        [Description("The verbosity level for this operation (low, med, high).")]
        [DefaultValue(VerbosityLevel.med)]
        public VerbosityLevel Verbose { get; set; } = VerbosityLevel.low;
    }
}
