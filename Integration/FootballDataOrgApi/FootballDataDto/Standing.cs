using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.FootballDataOrgApi.FootballDataDto
{
   
    public class Standing
    {
        public string Group { get; set; }
        public int Rank { get; set; }
        public string Team { get; set; }
        public int TeamId { get; set; }
        public int PlayedGames { get; set; }
        public string CrestUri { get; set; }
        public int Points { get; set; }
        public int Goals { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
    }


}
