using System;
using System.Collections.Generic;
using Dzaba.League.Contracts;
using Dzaba.Utils;

namespace Dzaba.League.Algorithms
{
    public static class Elo
    {
        public static Ranking<T> Build<T>(IEnumerable<IReadOnlyMatch<T>> matches, ScoreCheckOptions scoreCheckOptions, EloOptions eloOptions)
            where T : IEquatable<T>
        {
            Require.NotNull(matches, nameof(matches));
            Require.NotNull(eloOptions, nameof(eloOptions));

            var ranking = new Dictionary<T, double>();

            foreach (var match in matches)
            {
                HandleMatch(ranking, match, scoreCheckOptions, eloOptions);
            }

            return new Ranking<T>(ranking);
        }

        private static void HandleMatch<T>(Dictionary<T, double> ranking, IReadOnlyMatch<T> match,
            ScoreCheckOptions scoreCheckOptions, EloOptions eloOptions)
            where T : IEquatable<T>
        {
            var values = new Dictionary<T, double>();
            var sumFactor = 0.0;
            var scoreTypes = Check.MatchResults(match, scoreCheckOptions);

            foreach (var set in match.Sets)
            {
                foreach (var score in set)
                {
                    if (scoreTypes.ContainsCompetitor(score.CompetitorId))
                    {
                        var value = GetCurrentRating(ranking, score.CompetitorId, eloOptions);
                        value = Transform(eloOptions, value);
                        values.Add(score.CompetitorId, value);
                        sumFactor += value;
                    }
                }
            }

            foreach (var value in values)
            {
                var newValue = GetCurrentRating(ranking, value.Key, eloOptions) +
                               eloOptions.K * (GetSFactor(scoreTypes[value.Key], eloOptions) - value.Value / sumFactor);
                UpdateRating(ranking, value.Key, newValue);
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
