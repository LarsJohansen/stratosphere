using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entities
{
    public class UserOnLeague
    {
        public Int64 Id { get; set; }

        public Int64 FkUser { get; set; }

        public Int64 FkUserLeague { get; set; }

        public User User { get; set; }

        public UserLeague UserLeague { get; set; }

        public virtual ICollection<UserTieBreak> UserTieBreaks { get; set; }
     
    }
}
