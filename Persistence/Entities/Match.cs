using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entities
{
    public class Match
    {
        public Int64 Id { get; set; }

        public Int64 FkHomeTeam { get; set; }

        public Int64 FkAwayTeam { get; set; }

        public Int64 FkMatchDay { get; set; }

        public Int64 FkHomeStatistics { get; set; }

        public Int64 FkAwayStatistics { get; set; }

        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }

        public MatchDay MatchDay { get; set; }

        public ICollection<MatchStatistics> MatchStatistics { get; set; }
    }
}
