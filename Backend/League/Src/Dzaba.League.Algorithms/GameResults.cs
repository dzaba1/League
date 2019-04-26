using Dzaba.League.Contracts;
using System;
using System.Collections.Generic;

namespace Dzaba.League.Algorithms
{
    public class GameResults<T> : CompetitorsCollectionBase<T, GameResultType, GameResultEntry<T>>
        where T : IEquatable<T>
    {
        public GameResults(Dictionary<T, GameResultType> dictionary) : base(dictionary)
        {
        }

        protected override GameResultEntry<T> BuildFromDictEntry(KeyValuePair<T, GameResultType> keyValuePair)
        {
            return new GameResultEntry<T>(keyValuePair.Key, keyValuePair.Value);
        }
    }
}
