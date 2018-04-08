using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entities
{
    public class MatchDay
    {
        public Int64 Id { get; set; }

        public string Name { get; set; }

        public DateTime MatchDateTime { get; set; }

        public Int64 FkMatchRound { get; set; }

        public MatchRound MatchRound { get; set; }
    }
}
