using System;

namespace Dzaba.League.Algorithms
{
    public struct RatingEntry<T>
        where T : IEquatable<T>
    {
        public RatingEntry(T competitorId, double rating)
        {
            CompetitorId = competitorId;
            Rating = rating;
        }

        public T CompetitorId { get; }
        public double Rating { get; }
    }
}
