using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.FootballDataOrgApi.FootballDataDto
{
    public class LeagueTableDto
    {
        public string LeagueCaption { get; set; }

        public int MatchDay { get; set; }

        public GroupStandingsDto Standings { get; set; }
    
    }
}
