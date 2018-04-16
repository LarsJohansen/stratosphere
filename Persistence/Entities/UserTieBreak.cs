using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entities
{
    public class UserTieBreak
    {
        public Int64 Id { get; set; }

        public Int64 FkUserOnLeague { get; set; }

        public Int64 FkTeamOnCompetition { get; set; }

        public uint TieBreakPosition { get; set; }

        public virtual UserOnLeague UserOnLeague { get; set; }

        public virtual TeamOnCompetition TeamOnCompetition { get; set; }
    }
}
