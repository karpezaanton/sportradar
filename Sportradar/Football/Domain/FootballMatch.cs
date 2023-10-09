namespace Sportradar.Football.Domain
{
    public class FootballMatch
    {
        public FootballTeam HomeTeam { get; set; }
        public FootballTeam AwayTeam { get; set; }
        public DateTime MatchDateTime { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public int TotalGoals => HomeTeamScore + AwayTeamScore;

        public FootballMatch(FootballTeam homeTeam, FootballTeam awayTeam, DateTime matchDateTime)
        {
            if (homeTeam == null)
                throw new ArgumentNullException(nameof(homeTeam));
            if (awayTeam == null)
                throw new ArgumentNullException(nameof(awayTeam));

            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            MatchDateTime = matchDateTime;
            HomeTeamScore = 0;
            AwayTeamScore = 0;
        }

        public void UpdateScore(int homeTeamScore, int awayTeamScore)
        {
            if (homeTeamScore < 0)
                throw new ArgumentOutOfRangeException(nameof(homeTeamScore));
            if (awayTeamScore < 0)
                throw new ArgumentOutOfRangeException(nameof(awayTeamScore));

            HomeTeamScore = homeTeamScore;
            AwayTeamScore = awayTeamScore;
        }
    }
}