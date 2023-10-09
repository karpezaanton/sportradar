using Sportradar.Football.Domain;

namespace Sportradar.DataProviders.Interfaces
{
    public interface IFootballDataProvider
    {
        IEnumerable<FootballMatch> Matches();
        void StartMatch(FootballTeam homeTeam, FootballTeam awayTeam);
        void UpdateMatch(FootballTeam homeTeam, FootballTeam awayTeam, int homeTeamScore, int awayTeamScore);
        void FinishMatch(FootballTeam homeTeam, FootballTeam awayTeam);
    }
}