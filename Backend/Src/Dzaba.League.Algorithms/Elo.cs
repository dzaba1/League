using System;
using System.Collections.Generic;
using System.Linq;
using Dzaba.League.Contracts;
using Dzaba.Utils;

namespace Dzaba.League.Algorithms
{
    public static class Elo
    {
        public static Ranking<T> Probabilities<T>(Ranking<T> gameCompetitors, EloOptions eloOptions)
            where T : IEquatable<T>
        {
            Require.NotNull(gameCompetitors, nameof(gameCompetitors));
            Require.NotNull(eloOptions, nameof(eloOptions));

            var sumFactor = 0.0;
            var values = gameCompetitors.ToDictionary(r => r.CompetitorId, r => r.Rating);

            foreach (var rating in gameCompetitors)
            {
                var value = GetCurrentRating(values, rating.CompetitorId, eloOptions);
                value = Transform(eloOptions, value);
                values[rating.CompetitorId] = value;
                sumFactor += value;
            }

            var dict = values
                .Select(v => new {Id = v.Key, Value = v.Value / sumFactor})
                .ToDictionary(v => v.Id, v => v.Value);
            return new Ranking<T>(dict);
        }

        public static Ranking<T> Build<T>(IEnumerable<IReadOnlyMatch<T>> matches, ScoreCheckOptions scoreCheckOptions, EloOptions eloOptions)
            where T : IEquatable<T>
        {
            Require.NotNull(matches, nameof(matches));
            Require.NotNull(eloOptions, nameof(eloOptions));

            var ranking = new Dictionary<T, double>();

            foreach (var match in matches.OrderBy(m => m.TimePlayed))
            {
                HandleMatch(ranking, match, scoreCheckOptions, eloOptions);
            }

            return new Ranking<T>(ranking);
        }

        private static void HandleMatch<T>(Dictionary<T, double> ranking, IReadOnlyMatch<T> match,
            ScoreCheckOptions scoreCheckOptions, EloOptions eloOptions)
            where T : IEquatable<T>
        {
            var matchRanking = new Ranking<T>(match.Competitors
                .ToDictionary(c => c, c => GetCurrentRating(ranking, c, eloOptions)));

            var probabilities = Probabilities(matchRanking, eloOptions);
            var scoreTypes = Check.MatchResults(match, scoreCheckOptions);

            foreach (var probability in probabilities)
            {
                var currentRating = GetCurrentRating(ranking, probability.CompetitorId, eloOptions);
                var actual = GetSFactor(scoreTypes[probability.CompetitorId], eloOptions);
                var newValue = currentRating + eloOptions.K * (actual - probability.Rating);
                UpdateRating(ranking, probability.CompetitorId, newValue);
            }
        }

        private static double Transform(EloOptions options, double value)
        {
            var exp = value / options.ExponentialFactor;
            return Math.Pow(10, exp);
        }

        private static double GetSFactor(GameResultType scoreType, EloOptions options)
        {
            switch (scoreType)
            {
                case GameResultType.Won:
                    return options.Win;
                case GameResultType.Lost:
                    return options.Loss;
                case GameResultType.Draw:
                    return options.Draw;
                default: throw new InvalidOperationException($"Unknown value {scoreType}.");
            }
        }

        private static double GetCurrentRating<T>(Dictionary<T, double> ranking, T competitorId,
            EloOptions options)
            where T : IEquatable<T>
        {
            if (!ranking.TryGetValue(competitorId, out double rating))
            {
                rating = options.Initial;
                ranking.Add(competitorId, rating);
            }

            return rating;
        }

        private static void UpdateRating<T>(Dictionary<T, double> ranking, T competitorId, double value)
            where T : IEquatable<T>
        {
            if (ranking.ContainsKey(competitorId))
            {
                ranking[competitorId] = value;
            }
            else
            {
                ranking.Add(competitorId, value);
            }
        }
    }
}
