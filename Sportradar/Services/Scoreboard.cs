using Sportradar.DataProviders.Interfaces;
using Sportradar.Football.Domain;
using Sportradar.Services.Interfaces;

namespace Sportradar.Services
{
    public class Scoreboard : IScoreboard
    {
        private readonly IFootballDataProvider dataProvider;
        public Scoreboard(IFootballDataProvider dataProvider)
        {
            this.dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
        }

        public async void StartNewMatch(FootballTeam homeTeam, FootballTeam awayTeam)
        {
            if (homeTeam == null)
                throw new ArgumentNullException(nameof(homeTeam));
            if (awayTeam == null)
                throw new ArgumentNullException(nameof(awayTeam));

            await Task.Run(() => dataProvider.StartMatch(homeTeam, awayTeam));
        }

        public async void UpdateScore(FootballTeam homeTeam, FootballTeam awayTeam, int homeTeamScore, int awayTeamScore)
        {
            if (homeTeam == null)
                throw new ArgumentNullException(nameof(homeTeam));
            if (awayTeam == null)
                throw new ArgumentNullException(nameof(awayTeam));
            if (homeTeamScore < 0)
                throw new ArgumentOutOfRangeException(nameof(homeTeamScore));
            if (awayTeamScore < 0)
                throw new ArgumentOutOfRangeException(nameof(awayTeamScore));

            await Task.Run(() => dataProvider.UpdateMatch(homeTeam, awayTeam, homeTeamScore, awayTeamScore));
        }

        public async void FinishMatch(FootballTeam homeTeam, FootballTeam awayTeam)
        {
            if (homeTeam == null)
                throw new ArgumentNullException(nameof(homeTeam));
            if (awayTeam == null)
                throw new ArgumentNullException(nameof(awayTeam));

            await Task.Run(() => dataProvider.FinishMatch(homeTeam, awayTeam));
        }

        public async Task<IEnumerable<FootballMatch>> GetMatches(bool isAsc = false)
        {
            return await Task.Run(() => dataProvider.Matches(isAsc));
        }
    }
}
