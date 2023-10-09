using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sportradar.DataProviders.Interfaces;
using Sportradar.Football.Domain;
using Sportradar.Services;

namespace Sportradar.Tests
{
    [TestClass]
    public class ScoreboardTests
    {
        private Scoreboard scoreboard;
        private Mock<IFootballDataProvider> dataProviderMock;
        private readonly string HomeTeam = "HomeTeam";
        private readonly string AwayTeam = "AwayTeam";

        [TestInitialize]
        public void SetUp()
        {
            dataProviderMock = new Mock<IFootballDataProvider>();
            scoreboard = new Scoreboard(dataProviderMock.Object);
        }

        [TestMethod]
        public async Task GetMatches_ReturnsMatchesFromDataProvider()
        {
            // Arrange
            var expectedMatches = new List<FootballMatch>
            {
                new FootballMatch(new FootballTeam("HomeTeam1"), new FootballTeam("AwayTeam1"), DateTime.Now),
                new FootballMatch(new FootballTeam("HomeTeam2"), new FootballTeam("AwayTeam2"), DateTime.Now),
            };
            dataProviderMock.Setup(dp => dp.Matches()).Returns(expectedMatches);

            // Act
            var matches = await scoreboard.GetMatches();

            // Assert
            Assert.AreEqual(expectedMatches.Count, matches.Count());
            CollectionAssert.AreEqual(expectedMatches, matches.ToList());
        }

        [TestMethod]
        public void StartNewMatch_CallsDataProviderStartMatch()
        {
            // Arrange
            var homeTeam = new FootballTeam(HomeTeam);
            var awayTeam = new FootballTeam(AwayTeam);

            // Act
            scoreboard.StartNewMatch(homeTeam, awayTeam);

            // Assert
            dataProviderMock.Verify(dp => dp.StartMatch(It.Is<FootballTeam>(t => t.TeamName == HomeTeam), It.Is<FootballTeam>(t => t.TeamName == AwayTeam)), Times.Once);
        }

        [TestMethod]
        public void UpdateScore_CallsDataProviderUpdateMatch()
        {
            // Arrange
            var homeTeam = new FootballTeam(HomeTeam);
            var awayTeam = new FootballTeam(AwayTeam);

            // Act
            scoreboard.UpdateScore(homeTeam, awayTeam, 2, 1);

            // Assert
            dataProviderMock.Verify(dp => dp.UpdateMatch(It.Is<FootballTeam>(t => t.TeamName == HomeTeam), It.Is<FootballTeam>(t => t.TeamName == AwayTeam), 2, 1), Times.Once);
        }

        [TestMethod]
        public void FinishMatch_CallsDataProviderFinishMatch()
        {
            // Arrange
            var homeTeam = new FootballTeam(HomeTeam);
            var awayTeam = new FootballTeam(AwayTeam);

            // Act
            scoreboard.FinishMatch(homeTeam, awayTeam);

            // Assert
            dataProviderMock.Verify(dp => dp.FinishMatch(It.Is<FootballTeam>(t => t.TeamName == HomeTeam), It.Is<FootballTeam>(t => t.TeamName == AwayTeam)), Times.Once);
        }

        [TestMethod]
        public void StartNewMatch_ThrowsArgumentNullException_WhenHomeTeamIsNull()
        {
            // Arrange
            var awayTeam = new FootballTeam(AwayTeam);

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                scoreboard.StartNewMatch(null, awayTeam);
            });
        }

        [TestMethod]
        public void StartNewMatch_ThrowsArgumentNullException_WhenAwayTeamIsNull()
        {
            // Arrange
            var homeTeam = new FootballTeam(HomeTeam);

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                scoreboard.StartNewMatch(homeTeam, null);
            });
        }
    }
}