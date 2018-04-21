using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.FootballDataDto
{
    public class TeamsDto
    {
        public int Count { get; set; }

        public List<TeamDto> Teams { get; set; }
    }
}
