//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------

namespace D20Tek.CountryService.Models
{
    public class CountryCode
    {
        public string Name { get; set; } = string.Empty;

        public string OfficialName { get; set; } = string.Empty;

        public int Sovereignty { get; set; }

        public string Alpha2Code { get; set; } = string.Empty;

        public string Alpha3Code { get; set; } = string.Empty;

        public int CountryId { get; set; }

        public string TopLevelDomain { get; set; } = string.Empty;
    }
}
