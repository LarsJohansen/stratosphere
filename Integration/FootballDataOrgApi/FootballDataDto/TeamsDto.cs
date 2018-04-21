using System.Collections.Generic;

namespace Integration.FootballDataOrgApi.FootballDataDto
{
    public class TeamsDto
    {
        public int Count { get; set; }

        public List<TeamDto> Teams { get; set; }
    }
}
