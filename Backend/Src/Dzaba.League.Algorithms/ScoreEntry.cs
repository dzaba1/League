using System;

namespace Dzaba.League.Algorithms
{
    public struct ScoreEntry<T>
        where T : IEquatable<T>
    {
        public ScoreEntry(T competitorId, int score)
        {
            CompetitorId = competitorId;
            Score = score;
        }

        public T CompetitorId { get; }
        public int Score { get; }
    }
}
