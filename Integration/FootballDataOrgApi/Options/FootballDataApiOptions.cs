﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.FootballDataOrgApi.Options
{
    public class FootballDataApiOptions
    {
        public string BaseUri { get; set; }

        public string TeamEndpoint { get; set; }

        public string CompetitionEndpoint { get; set; }

        public string LeagueTableEndpoint { get; set; }
     
    }
}