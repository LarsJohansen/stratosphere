using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entities
{
    public class UserMatchPrediction
    {
        public Int64 Id { get; set; }

        public Int64 FkUserOnLeague { get; set; }

        public Int64 FkMatch { get; set; }

        public int NumberOfGoalsHomeTeam { get; set; }

        public int NumberOfGoalsAwayTeam { get; set; }

        public virtual UserOnLeague UserOnLeague { get; set; }

        public virtual Match Match { get; set; }
    }
}
