using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Persistence.Entities
{
    public class UserLeague
    {
        public Int64 Id { get; set; }

        public string LeagueIdentifier { get; set; }

        public string Name { get; set; }

        public Int64 FkUserAdmin { get; set; }

        public Int64 FkCompetition { get; set; }

        public virtual User UserAdmin { get; set; }
        
        public virtual Competition Competition { get; set; }

        public virtual ICollection<UserOnLeague> UserOnLeagues { get; set; }
    }
}
