using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entities
{
    public class Team
    {
        public Int64 Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string CrestUri { get; set; }

        public int ExternalId { get; set; }



        public Int64 FkCompetition { get; set; }

        public Int64 FkGroup { get; set; }

        public virtual Competition Competition { get; set; }

        public virtual Group Group { get; set; }

        public virtual ICollection<Match> HomeMatches { get; set; }

        public virtual ICollection<Match> AwayMatches { get; set; }

        public virtual ICollection<MatchStatistics> MatchStatistics { get; set; }

        public virtual  ICollection<TeamOnCompetition> TeamOnCompetitions { get; set; }

    }
}
