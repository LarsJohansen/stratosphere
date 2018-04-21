using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.FootballDataOrgApi.FootballDataDto
{
    public class LeagueTable
    {
        public string LeagueCaption { get; set; }

        public int MatchDay { get; set; }

        public Standings Standings { get; set; }
    
    }
}
