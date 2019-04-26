using Dzaba.League.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dzaba.League.Algorithms
{
    public class GameResults<T> : IEnumerable<GameResultEntry<T>>
        where T : IEquatable<T>
    {
        public GameResults(Dictionary<T, GameResultType> dictionary)
        {
            Require.NotNull(dictionary, nameof(dictionary));

            Dictionary = dictionary;
        }

        protected Dictionary<T, GameResultType> Dictionary { get; }

        public IEnumerator<GameResultEntry<T>> GetEnumerator()
        {
            return Dictionary
                .Select(p => new GameResultEntry<T>(p.Key, p.Value))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
