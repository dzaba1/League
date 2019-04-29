using System;
using System.Collections.Generic;
using System.Linq;

namespace Dzaba.League.Algorithms
{
    public interface IReadOnlyMatch<T>
        where T : IEquatable<T>
    {
        DateTime TimePlayed { get; }
        IEnumerable<Scores<T>> Sets { get; }
        IEnumerable<T> Competitors { get; }
    }

    public sealed class Match<T> : IReadOnlyMatch<T>
        where T : IEquatable<T>
    {
        private readonly HashSet<T> competitors = new HashSet<T>();
        private readonly List<Dictionary<T, int>> sets = new List<Dictionary<T, int>>();

        public Match(DateTime time)
        {
            TimePlayed = time;
        }

        public DateTime TimePlayed { get; }
        public IEnumerable<Scores<T>> Sets => sets.Select(s => new Scores<T>(s));
        public IEnumerable<T> Competitors => competitors;

        public void AddCompetitor(T competitorId)
        {
            competitors.Add(competitorId);
        }

        public void AddScores(T competitorId, params int[] scores)
        {
            ValidateCompetitor(competitorId);
            ValidateScores(scores);

            if (sets.Count > 0)
            {
                for (int i = 0; i < scores.Length; i++)
                {
                    var score = scores[i];
                    var set = sets[i];

                    set.Add(competitorId, score);
                }
            }
            else
            {
                foreach (var score in scores)
                {
                    var dict = new Dictionary<T, int>
                    {
                        {competitorId, score}
                    };
                    sets.Add(dict);
                }
            }
        }

        private void ValidateScores(int[] scores)
        {
            if (sets.Count > 0 && scores.Length != sets.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(scores),
                    $"Invalid number of sets. Expected {sets.Count} but got {scores.Length}.");
            }
        }

        private void ValidateCompetitor(T competitorId)
        {
            if (!competitors.Contains(competitorId))
            {
                throw new KeyNotFoundException($"The competitor with ID {competitorId} is not added.");
            }
        }
    }
}
