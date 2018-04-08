using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entities
{
    public class Team
    {
        public Int64 Id { get; set; }

        public string Name { get; set; }

        public Int64 FkCompetition { get; set; }

        public Int64 FkGroup { get; set; }

        public virtual Competition Competition { get; set; }

        public virtual Group Group { get; set; }

        public virtual ICollection<Match> Matches { get; set; }

        public virtual ICollection<MatchStatistics> MatchStatistics { get; set; }

    }
}
