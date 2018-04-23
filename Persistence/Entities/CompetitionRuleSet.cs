using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entities
{
    public class CompetitionRuleSet
    {
        public Int64 Id { get; set; }

        public int NumberOfTeamsToPlayOffPerGroup { get; set; }

        public string LeagueDescription { get; set; }
    }
}
