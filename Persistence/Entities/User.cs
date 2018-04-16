using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entities
{
    public class User
    {
        public Int64 Id { get; set; }

        public string Email { get; set; }

        public string Firstname { get; set; }
        
        public string Lastname { get; set; }

        public DateTime Created { get; set; }

        public bool Enabled { get; set; }

        public virtual ICollection<UserLeague> UserLeaguesWhereAdmin { get; set; }

        public virtual ICollection<UserOnLeague> UserOnLeagues { get; set; }

        public virtual UserCompetitionScore UserCompetitionScore { get; set; }

    }
}
