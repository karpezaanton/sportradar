using Sportradar.DataProviders.Interfaces;
using Sportradar.Football.Domain;

namespace Sportradar.DataProviders
{
    public class FootballDataProvider : IFootballDataProvider
    {
        private readonly object syncRoot = new object();
        private List<FootballMatch> matches;

        public FootballDataProvider() {
            matches = new List<FootballMatch>();
        }

        public IEnumerable<FootballMatch> Matches()
        {
            return matches;
        }

        public void StartMatch(FootballTeam homeTeam, FootballTeam awayTeam)
        {
            if (homeTeam == null)
                throw new ArgumentNullException(nameof(homeTeam));
            if (awayTeam == null)
                throw new ArgumentNullException(nameof(awayTeam));

            lock (syncRoot)
            {
                AddMatch(homeTeam, awayTeam);
                ReorderMatches();
            }
        }

        public void UpdateMatch(FootballTeam homeTeam, FootballTeam awayTeam, int homeTeamScore, int awayTeamScore)
        {
            if (homeTeam == null)
                throw new ArgumentNullException(nameof(homeTeam));
            if (awayTeam == null)
                throw new ArgumentNullException(nameof(awayTeam));
            if (homeTeamScore < 0)
                throw new ArgumentOutOfRangeException(nameof(homeTeamScore));
            if (awayTeamScore < 0)
                throw new ArgumentOutOfRangeException(nameof(awayTeamScore));

            lock (syncRoot)
            {
                var match = GetMatch(homeTeam, awayTeam);

                if (match == null)
                    throw new InvalidOperationException("Match not found.");

                match.UpdateScore(homeTeamScore, awayTeamScore);
                ReorderMatches();
            }
        }

        public void FinishMatch(FootballTeam homeTeam, FootballTeam awayTeam)
        {
            if (homeTeam == null)
                throw new ArgumentNullException(nameof(homeTeam));
            if (awayTeam == null)
                throw new ArgumentNullException(nameof(awayTeam));

            lock (syncRoot)
            {
                var match = GetMatch(homeTeam, awayTeam);

                if (match == null)
                    throw new InvalidOperationException("Match not found.");

                matches.Remove(match);
            }
        }

        private void AddMatch(FootballTeam homeTeam, FootballTeam awayTeam)
        {
            var matchExists = GetMatch(homeTeam, awayTeam);

            if (matchExists != null)
                throw new InvalidOperationException("Match already exists.");

            var match = new FootballMatch(homeTeam, awayTeam, DateTime.UtcNow);
            matches.Add(match);
        }

        private FootballMatch? GetMatch(FootballTeam homeTeam, FootballTeam awayTeam)
        {
            return matches.FirstOrDefault(m => m.HomeTeam.TeamName == homeTeam.TeamName && m.AwayTeam.TeamName == awayTeam.TeamName);
        }

        private void ReorderMatches()
        {
            if (!matches.Any())
                return;

            matches = matches
                .OrderByDescending(m => m.TotalGoals)
                .ThenByDescending(m => m.MatchDateTime)
                .ToList();
        }
    }
}
