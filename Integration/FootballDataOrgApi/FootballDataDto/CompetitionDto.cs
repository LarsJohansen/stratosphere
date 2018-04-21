﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Models
{
    public class CompetitionDto
    {
        public Int64 Id { get; set; }

        public string Caption { get; set; }

        public string League { get; set; }

        public string Year { get; set; }

        public int NumberOfTeams { get; set; }

        public int NumberOfGames { get; set; }
    }
}