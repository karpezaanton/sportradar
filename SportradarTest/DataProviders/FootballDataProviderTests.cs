using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sportradar.DataProviders;
using Sportradar.Football.Domain;

namespace Sportradar.Tests
{
    [TestClass]
    public class FootballDataProviderTests
    {
        private FootballDataProvider dataProvider;
        private readonly string HomeTeam = "HomeTeam";
        private readonly string AwayTeam = "AwayTeam";

        [TestInitialize]
        public void SetUp()
        {
            dataProvider = new FootballDataProvider();
        }

        [TestMethod]
        public void StartMatch_AddsMatchToMatchesList()
        {
            // Arrange
            var homeTeam = new FootballTeam(HomeTeam);
            var awayTeam = new FootballTeam(AwayTeam);

            // Act
            dataProvider.StartMatch(homeTeam, awayTeam);

            // Assert
            var matches = dataProvider.Matches().ToList();
            Assert.AreEqual(1, matches.Count);
            Assert.AreEqual(HomeTeam, matches[0].HomeTeam.TeamName);
            Assert.AreEqual(AwayTeam, matches[0].AwayTeam.TeamName);
        }

        [TestMethod]
        public void StartMatch_ThrowsArgumentNullException_WhenHomeTeamIsNull()
        {
            // Arrange
            var awayTeam = new FootballTeam(AwayTeam);

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                dataProvider.StartMatch(null, awayTeam);
            });
        }

        [TestMethod]
        public void StartMatch_ThrowsArgumentNullException_WhenAwayTeamIsNull()
        {
            // Arrange
            var homeTeam = new FootballTeam(HomeTeam);

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                dataProvider.StartMatch(homeTeam, null);
            });
        }

        [TestMethod]
        public void UpdateMatch_UpdatesMatchScore()
        {
            // Arrange
            var homeTeam = new FootballTeam(HomeTeam);
            var awayTeam = new FootballTeam(AwayTeam);
            dataProvider.StartMatch(homeTeam, awayTeam);

            // Act
            dataProvider.UpdateMatch(homeTeam, awayTeam, 2, 1);

            // Assert
            var match = dataProvider.Matches().FirstOrDefault();
            Assert.IsNotNull(match);
            Assert.AreEqual(2, match.HomeTeamScore);
            Assert.AreEqual(1, match.AwayTeamScore);
        }

        [TestMethod]
        public void UpdateMatch_ThrowsArgumentNullException_WhenHomeTeamIsNull()
        {
            // Arrange
            var awayTeam = new FootballTeam(AwayTeam);

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                dataProvider.UpdateMatch(null, awayTeam, 2, 1);
            });
        }

        [TestMethod]
        public void UpdateMatch_ThrowsArgumentNullException_WhenAwayTeamIsNull()
        {
            // Arrange
            var homeTeam = new FootballTeam(HomeTeam);

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                dataProvider.UpdateMatch(homeTeam, null, 2, 1);
            });
        }

        [TestMethod]
        public void UpdateMatch_ThrowsArgumentOutOfRangeException_WhenHomeTeamScoreIsNegative()
        {
            // Arrange
            var homeTeam = new FootballTeam(HomeTeam);
            var awayTeam = new FootballTeam(AwayTeam);
            dataProvider.StartMatch(homeTeam, awayTeam);

            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                dataProvider.UpdateMatch(homeTeam, awayTeam, -1, 1);
            });
        }

        [TestMethod]
        public void UpdateMatch_ThrowsArgumentOutOfRangeException_WhenAwayTeamScoreIsNegative()
        {
            // Arrange
            var homeTeam = new FootballTeam(HomeTeam);
            var awayTeam = new FootballTeam(AwayTeam);
            dataProvider.StartMatch(homeTeam, awayTeam);

            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                dataProvider.UpdateMatch(homeTeam, awayTeam, 1, -1);
            });
        }
    }
}