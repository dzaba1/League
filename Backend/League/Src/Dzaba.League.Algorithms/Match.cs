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

    public sealed class Match<T>
        where T : IEquatable<T>
    {
        public Match(DateTime time, IEnumerable<MatchScore<T>> scores)
        {
            Require.NotNull(scores, nameof(scores));

            Time = time;
            Scores = scores.ToArray();
        }

        public DateTime Time { get; }

        public MatchScore<T>[] Scores { get; }
    }
}
