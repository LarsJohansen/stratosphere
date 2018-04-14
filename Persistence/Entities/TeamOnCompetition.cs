using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entities
{
    public class TeamOnCompetition
    {
        public Int64 Id { get; set; }

        public Int64 FkTeam { get; set; }

        public Int64 FkCompetition { get; set; }

        public int? GroupTieBreakPosition { get; set; }

        public virtual Team Team { get; set; }

        public virtual Competition Competition { get; set; }
    }
}
