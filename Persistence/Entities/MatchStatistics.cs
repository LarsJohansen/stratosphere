using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entities
{
    public class MatchStatistics
    {
        public Int64 Id { get; set; }

        public Int64 FkTeam { get; set; }

        public Int64 FkMatch { get; set; }

        public int NumberOfGoals { get; set; }

        public int NumberOfRedCards { get; set; }

        public int NumberOfYellowCards { get; set; }

        public bool HomeTeam { get; set; }

        public virtual Team Team { get; set; }

        public virtual Match Match { get; set; }

        

    }
}
