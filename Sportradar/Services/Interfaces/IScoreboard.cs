using Sportradar.Football.Domain;

namespace Sportradar.Services.Interfaces
{
    public interface IScoreboard
    {
        void StartNewMatch(FootballTeam homeTeam, FootballTeam awayTeam);
        void UpdateScore(FootballTeam homeTeam, FootballTeam awayTeam, int homeTeamScore, int awayTeamScore);
        void FinishMatch(FootballTeam homeTeam, FootballTeam awayTeam);
        Task<IEnumerable<FootballMatch>> GetMatches(bool isAsc);
    }
}
