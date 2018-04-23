using System;
using System.Collections.Generic;
using System.Text;
using Persistence.Abstract;
using Persistence.Entities;

namespace Persistence
{
    public class StratosphereUnitOfWork : IStratosphereUnitOfWork
    {
        private readonly StratosphereContext _context;

        public StratosphereUnitOfWork(StratosphereContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Initialize();
        }

        private void Initialize()
        {
            Competitions = new Repository<Competition>(_context);
            CompetitionSetups = new Repository<CompetitionSetup>(_context);
            CompetitionRuleSets = new Repository<CompetitionRuleSet>(_context);
            Groups = new Repository<Group>(_context);
            Matches = new Repository<Match>(_context);
            MatchDays = new Repository<MatchDay>(_context);
            MatchRounds = new Repository<MatchRound>(_context);
            MatchStatistics = new Repository<MatchStatistics>(_context);
            Teams = new Repository<Team>(_context);
            TeamOnCompetitions = new Repository<TeamOnCompetition>(_context);
            Users = new Repository<User>(_context);
            UserCompetitionScores = new Repository<UserCompetitionScore>(_context);
            UserLeagues = new Repository<UserLeague>(_context);
            UserMatchPredictions = new Repository<UserMatchPrediction>(_context);
            UserOnLeagues = new Repository<UserOnLeague>(_context);
            UserTieBreaks = new Repository<UserTieBreak>(_context);
        }

        public IRepository<Competition> Competitions { get; private set; }
        public IRepository<CompetitionSetup> CompetitionSetups { get; private set; }
        public IRepository<CompetitionRuleSet> CompetitionRuleSets { get; private set; }
        public IRepository<Group> Groups { get; private set; }
        public IRepository<Match> Matches { get; private set; }
        public IRepository<MatchDay> MatchDays { get; private set; }
        public IRepository<MatchRound> MatchRounds { get; private set; }
        public IRepository<MatchStatistics> MatchStatistics { get; private set; }
        public IRepository<Team> Teams { get; private set; }
        public IRepository<TeamOnCompetition> TeamOnCompetitions { get; private set; }
        public IRepository<User> Users { get; private set; }
        public IRepository<UserCompetitionScore> UserCompetitionScores { get; private set; }
        public IRepository<UserLeague> UserLeagues { get; private set; }
        public IRepository<UserMatchPrediction> UserMatchPredictions { get; private set; }
        public IRepository<UserOnLeague> UserOnLeagues { get; private set; }
        public IRepository<UserTieBreak> UserTieBreaks { get; private set; }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
