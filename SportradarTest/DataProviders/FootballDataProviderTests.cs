using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sportradar.DataProviders;
using Sportradar.Football.Domain;

namespace Sportradar.Tests
{
    [TestClass]
    public class FootballDataProviderTests
    {
        private FootballDataProvider dataProvider;
        private readonly string HomeTeamFirst = "HomeTeam_1";
        private readonly string AwayTeamFirst = "AwayTeam_1";
        private readonly string HomeTeamSecond = "HomeTeam_2";
        private readonly string AwayTeamSecond = "AwayTeam_2";

        [TestInitialize]
        public void SetUp()
        {
            dataProvider = new FootballDataProvider();
        }

        [TestMethod]
        public void StartMatch_AddsMatchesToMatchesList_Asc()
        {
            // Arrange
            var homeTeam1 = new FootballTeam(HomeTeamFirst);
            var awayTeam1 = new FootballTeam(AwayTeamFirst);
            var homeTeam2 = new FootballTeam(HomeTeamSecond);
            var awayTeam2 = new FootballTeam(AwayTeamSecond);

            // Act
            dataProvider.StartMatch(homeTeam1, awayTeam1);
            dataProvider.StartMatch(homeTeam2, awayTeam2);

            // Assert
            var matches = dataProvider.Matches(true).ToList();
            Assert.AreEqual(2, matches.Count);
            Assert.AreEqual(HomeTeamFirst, matches[0].HomeTeam.TeamName);
            Assert.AreEqual(AwayTeamFirst, matches[0].AwayTeam.TeamName);
        }

        [TestMethod]
        public void StartMatch_AddsMatchesToMatchesList_Desc()
        {
            // Arrange
            var homeTeam1 = new FootballTeam(HomeTeamFirst);
            var awayTeam1 = new FootballTeam(AwayTeamFirst);
            var homeTeam2 = new FootballTeam(HomeTeamSecond);
            var awayTeam2 = new FootballTeam(AwayTeamSecond);

            // Act
            dataProvider.StartMatch(homeTeam1, awayTeam1);
            dataProvider.StartMatch(homeTeam2, awayTeam2);

            // Assert
            var matches = dataProvider.Matches(false).ToList();
            Assert.AreEqual(2, matches.Count);
            Assert.AreEqual(HomeTeamSecond, matches[0].HomeTeam.TeamName);
            Assert.AreEqual(AwayTeamSecond, matches[0].AwayTeam.TeamName);
        }

        [TestMethod]
        public void StartMatch_AddsMatchesToMatchesList_UpdateFirstMatch_Desc()
        {
            // Arrange
            var homeTeam1 = new FootballTeam(HomeTeamFirst);
            var awayTeam1 = new FootballTeam(AwayTeamFirst);
            var homeTeam2 = new FootballTeam(HomeTeamSecond);
            var awayTeam2 = new FootballTeam(AwayTeamSecond);

            // Act
            dataProvider.StartMatch(homeTeam1, awayTeam1);
            dataProvider.StartMatch(homeTeam2, awayTeam2);
            dataProvider.UpdateMatch(homeTeam1, awayTeam1, 2, 1);

            // Assert
            var matches = dataProvider.Matches(false).ToList();
            Assert.AreEqual(2, matches.Count);
            Assert.AreEqual(HomeTeamFirst, matches[0].HomeTeam.TeamName);
            Assert.AreEqual(AwayTeamFirst, matches[0].AwayTeam.TeamName);
        }

        [TestMethod]
        public void StartMatch_ThrowsArgumentNullException_WhenHomeTeamIsNull()
        {
            // Arrange
            var awayTeam = new FootballTeam(AwayTeamFirst);

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
            var homeTeam = new FootballTeam(HomeTeamFirst);

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
            var homeTeam = new FootballTeam(HomeTeamFirst);
            var awayTeam = new FootballTeam(AwayTeamFirst);
            dataProvider.StartMatch(homeTeam, awayTeam);

            // Act
            dataProvider.UpdateMatch(homeTeam, awayTeam, 2, 1);

            // Assert
            var match = dataProvider.Matches(false).FirstOrDefault();
            Assert.IsNotNull(match);
            Assert.AreEqual(2, match.HomeTeamScore);
            Assert.AreEqual(1, match.AwayTeamScore);
        }

        [TestMethod]
        public void UpdateMatch_ThrowsArgumentNullException_WhenHomeTeamIsNull()
        {
            // Arrange
            var awayTeam = new FootballTeam(AwayTeamFirst);

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
            var homeTeam = new FootballTeam(HomeTeamFirst);

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
            var homeTeam = new FootballTeam(HomeTeamFirst);
            var awayTeam = new FootballTeam(AwayTeamFirst);
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
            var homeTeam = new FootballTeam(HomeTeamFirst);
            var awayTeam = new FootballTeam(AwayTeamFirst);
            dataProvider.StartMatch(homeTeam, awayTeam);

            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                dataProvider.UpdateMatch(homeTeam, awayTeam, 1, -1);
            });
        }
    }
}