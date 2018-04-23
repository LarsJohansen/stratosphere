using System;
using System.Collections.Generic;
using System.Text;
using Persistence.Entities;

namespace Persistence.Abstract
{
    public interface IStratosphereUnitOfWork : IDisposable
    {
        IRepository<Competition> Competitions { get; }

        IRepository<CompetitionSetup> CompetitionSetups { get; }

        IRepository<CompetitionRuleSet> CompetitionRuleSets { get; }
        
        IRepository<Group> Groups { get; }

        IRepository<Match> Matches { get; }

        IRepository<MatchDay> MatchDays { get; }

        IRepository<MatchRound> MatchRounds { get; }

        IRepository<MatchStatistics> MatchStatistics { get; }

        IRepository<Team> Teams { get; }

        IRepository<TeamOnCompetition> TeamOnCompetitions { get; }

        IRepository<User> Users { get; }

        IRepository<UserCompetitionScore> UserCompetitionScores { get; }

        IRepository<UserLeague> UserLeagues { get; }

        IRepository<UserMatchPrediction> UserMatchPredictions { get;  }

        IRepository<UserOnLeague> UserOnLeagues { get; }

        IRepository<UserTieBreak> UserTieBreaks { get;  }

        int Complete();
    }
}
