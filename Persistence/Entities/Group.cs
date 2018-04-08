using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entities
{
    public class Group
    {
        public Int64 Id { get; set; }

        public string Name { get; set; }

        public Int64 FkCompetition { get; set; }

        public virtual Competition Competition { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

    }
}
