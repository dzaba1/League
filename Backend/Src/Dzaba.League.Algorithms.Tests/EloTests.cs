using System;
using Dzaba.League.Contracts;
using FluentAssertions;
using NUnit.Framework;

namespace Dzaba.League.Algorithms.Tests
{
    [TestFixture]
    public class EloTests
    {
        [Test]
        public void Build_WhenMatchProvided_ThenItBuildRanking()
        {
            var team1Id = 1;
            var team2Id = 2;

            var match = new Match<int>(DateTime.Now);
            match.AddCompetitor(team1Id);
            match.AddCompetitor(team2Id);
            match.AddScores(team1Id, 10, 8, 10);
            match.AddScores(team2Id, 7, 10, 5);

            var ranking = Elo.Build(new[] {match}, ScoreCheckOptions.DrawOnExAequoWin, EloOptions.Default);
            ranking.Count.Should().Be(2);
            ranking[team1Id].Should().Be(1016);
            ranking[team2Id].Should().Be(984);
        }
    }
}
