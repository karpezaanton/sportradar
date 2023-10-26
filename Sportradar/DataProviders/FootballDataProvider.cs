using Sportradar.DataProviders.Interfaces;
using Sportradar.Football.Domain;
using System.Collections.Concurrent;

namespace Sportradar.DataProviders
{
    public class FootballDataProvider : IFootballDataProvider
    {
        private BlockingCollection<FootballMatch> matches;

        public FootballDataProvider()
        {
            matches = new BlockingCollection<FootballMatch>();
        }

        public IEnumerable<FootballMatch> Matches(bool isAsc)
        {
            if (isAsc)
            {
                return matches
                    .OrderBy(m => m.TotalGoals)
                    .ThenBy(m => m.MatchDateTime);
            }
            else
            {
                return matches
                .OrderByDescending(m => m.TotalGoals)
                .ThenByDescending(m => m.MatchDateTime);
            }
        }

        public void StartMatch(FootballTeam homeTeam, FootballTeam awayTeam)
        {
            if (homeTeam == null)
                throw new ArgumentNullException(nameof(homeTeam));
            if (awayTeam == null)
                throw new ArgumentNullException(nameof(awayTeam));

            AddMatch(homeTeam, awayTeam);
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

            var match = RemoveMatch(homeTeam, awayTeam);

            if (match == null)
                throw new InvalidOperationException("Match not found.");

            match.UpdateScore(homeTeamScore, awayTeamScore);
            AddMatch(match);
        }

        public void FinishMatch(FootballTeam homeTeam, FootballTeam awayTeam)
        {
            if (homeTeam == null)
                throw new ArgumentNullException(nameof(homeTeam));
            if (awayTeam == null)
                throw new ArgumentNullException(nameof(awayTeam));

            RemoveMatch(homeTeam, awayTeam);
        }

        private void AddMatch(FootballTeam homeTeam, FootballTeam awayTeam)
        {
            var matchExists = GetMatch(homeTeam, awayTeam);

            if (matchExists != null)
                throw new InvalidOperationException("Match already exists.");

            AddMatch(new FootballMatch(homeTeam, awayTeam, DateTime.UtcNow));
        }

        private void AddMatch(FootballMatch match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            matches.Add(match);
        }

        private FootballMatch? RemoveMatch(FootballTeam homeTeam, FootballTeam awayTeam)
        {
            FootballMatch? match = GetMatch(homeTeam, awayTeam);

            if (match == null)
                throw new InvalidOperationException("Match not found.");

            matches.TryTake(out match);

            return match;
        }

        private FootballMatch? GetMatch(FootballTeam homeTeam, FootballTeam awayTeam)
        {
            return matches.FirstOrDefault(m => m.HomeTeam.TeamName == homeTeam.TeamName && m.AwayTeam.TeamName == awayTeam.TeamName);
        }
    }
}
