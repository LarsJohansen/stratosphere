using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entities
{
    public class UserCompetitionScore
    {
        public Int64 Id { get; set; }

        public int Score { get; set; }

        public Int64 FkUser { get; set; }

        public Int64 FkCompetition { get; set; }

        public virtual User User { get; set; }

        public virtual Competition Competition { get; set; }
    }
}
