using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entities
{
    public class CompetitionSetup
    {
        public Int64 Id { get; set; }

        public int NumberOfTeams { get; set; }

        public int NumberOfGroups { get; set; }

        public int NumberOfTeamsToPlayOff { get; set; }

        public Int64 FkCompetition { get; set; }

        public virtual Competition Competition { get; set; }
    }
}
