using System;
using System.Collections.Generic;
using System.Linq;

namespace Dzaba.League.Algorithms
{
    public sealed class MatchScore<T>
        where T : IEquatable<T>
    {
        public T CompetitorId { get; set; }
        public int[] Scores { get; set; }
    }

    public interface IReadOnlyMatch<T>
        where T : IEquatable<T>
    {
        DateTime TimePlayed { get; }
        IEnumerable<Scores<T>> Sets { get; }
        IEnumerable<T> Competitors { get; }
    }

    public sealed class Match<T>
        where T : IEquatable<T>
    {
        public Match(DateTime time)
        {
            TimePlayed = time;
        }

        public DateTime TimePlayed { get; }
    }
}
