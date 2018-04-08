using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entities
{
    public class Competition
    {
        public Int64 Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CreatedAt { get; set; }

        public bool Enabled { get; set; }

        public virtual ICollection<UserLeague> UserLeagues { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public virtual ICollection<MatchRound> MatchRounds { get; set; }
    }
}
