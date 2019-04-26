using System;
using Dzaba.League.Contracts;

namespace Dzaba.League.Algorithms
{
    public struct GameResultEntry<T>
        where T : IEquatable<T>
    {
        public GameResultEntry(T competitorId, GameResultType type)
        {
            CompetitorId = competitorId;
            Type = type;
        }

        public T CompetitorId { get; }
        public GameResultType Type { get; }
    }
}
