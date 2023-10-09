namespace Sportradar.Football.Domain
{
    public class FootballTeam
    {
        public string TeamName { get; set; }

        public FootballTeam(string teamName)
        {
            if (string.IsNullOrWhiteSpace(teamName))
                throw new ArgumentOutOfRangeException(nameof(teamName));

            TeamName = teamName;
        }
    }
}