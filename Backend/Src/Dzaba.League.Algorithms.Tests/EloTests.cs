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
        public void Build_WhenMatchProvided_ThenItBuildsRanking()
        {
            var team1Id = 1;
            var team2Id = 2;

            var match = BuildBaseMatch(DateTime.Now, team1Id, team2Id);
            match.AddScores(team1Id, 10, 8, 10);
            match.AddScores(team2Id, 7, 10, 5);

            var ranking = Elo.Build(new[] {match}, ScoreCheckOptions.DrawOnExAequoWin, EloOptions.Default);
            ranking.Count.Should().Be(2);
            ranking[team1Id].Should().Be(1016);
            ranking[team2Id].Should().Be(984);
        }

        private Match<int> BuildBaseMatch(DateTime timePlayed, params int[] ids)
        {
            var match = new Match<int>(timePlayed);
            foreach (var id in ids)
            {
                match.AddCompetitor(id);
            }

            return match;
        }

        [Test]
        public void Build_MultipleTeamsProvided_ThenItBuildsRanking()
        {
            var team1Id = 1;
            var team2Id = 2;
            var team3Id = 3;

            var match1 = BuildBaseMatch(DateTime.Now.AddDays(-1), team1Id, team2Id);
            match1.AddScores(team1Id, 10, 8, 10);
            match1.AddScores(team2Id, 7, 10, 5);

            var match2 = BuildBaseMatch(DateTime.Now, team1Id, team3Id);
            match2.AddScores(team1Id, 10, 8, 10);
            match2.AddScores(team3Id, 7, 10, 5);

            var ranking = Elo.Build(new[] { match1, match2 }, ScoreCheckOptions.DrawOnExAequoWin, EloOptions.Default);
            ranking.Count.Should().Be(3);
        }
    }
}
