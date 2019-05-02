using System;
using System.Collections.Generic;
using System.Linq;
using Dzaba.League.Contracts;
using Dzaba.Utils;

namespace Dzaba.League.Algorithms
{
    public static class Check
    {
        public static GameResults<T> GameResults<T>(Scores<T> scores, ScoreCheckOptions options)
            where T : IEquatable<T>
        {
            Require.NotNull(scores, nameof(scores));

            if (scores.Count < 2)
            {
                throw new ArgumentException("There should be at least 2 competitors", nameof(scores));
            }

            var result = new Dictionary<T, GameResultType>();
            var max = scores.Max(s => s.Score);
            var hasDraw = scores.Count(s => s.Score == max) > 1;
            var min = scores.Min(s => s.Score);
            var wonType = GameResultType.Won;

            if (options.HasFlag(ScoreCheckOptions.DrawOnExAequoWin) && hasDraw)
            {
                wonType = GameResultType.Draw;
            }

            if (options.HasFlag(ScoreCheckOptions.OneWinnerRestAreLoosers))
            {
                foreach (var score in scores)
                {
                    if (score.Score == max)
                    {
                        result.Add(score.CompetitorId, wonType);
                    }
                    else if (!options.HasFlag(ScoreCheckOptions.OnlyWinners))
                    {
                        result.Add(score.CompetitorId, GameResultType.Lost);
                    }
                }
            }
            else if (options.HasFlag(ScoreCheckOptions.OneLooserRestAreWinners))
            {
                foreach (var score in scores)
                {
                    if (score.Score == min && !options.HasFlag(ScoreCheckOptions.OnlyWinners))
                    {
                        result.Add(score.CompetitorId, GameResultType.Lost);
                    }
                    else if (score.Score > min)
                    {
                        result.Add(score.CompetitorId, wonType);
                    }
                }
            }
            else
            {
                foreach (var score in scores)
                {
                    if (score.Score == max)
                    {
                        result.Add(score.CompetitorId, wonType);
                    }
                    else if (score.Score == min && !options.HasFlag(ScoreCheckOptions.OnlyWinners))
                    {
                        result.Add(score.CompetitorId, GameResultType.Lost);
                    }
                }
            }

            return new GameResults<T>(result);
        }

        public static GameResults<T> MatchResults<T>(IReadOnlyMatch<T> match, ScoreCheckOptions options)
            where T : IEquatable<T>
        {
            Require.NotNull(match, nameof(match));

            var setResults = match.Sets
                .Select(s => GameResults(s, options));

            var dict = match.Competitors.ToDictionary(i => i, i => GameResultType.Draw);

            foreach (var setResult in setResults)
            {
                foreach (var result in setResult)
                {
                    var value = (int) dict[result.CompetitorId] + (int) result.Type;
                    if (value < -1)
                    {
                        value = -1;
                    }
                    else if (value > 1)
                    {
                        value = 1;
                    }

                    dict[result.CompetitorId] = (GameResultType) value;
                }
            }

            return new GameResults<T>(dict);
        }
    }
}
