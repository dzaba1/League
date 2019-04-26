using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dzaba.League.Algorithms
{
    public class Scores<T> : IEnumerable<ScoreEntry<T>>
        where T : IEquatable<T>
    {
        public Scores(Dictionary<T, int> dictionary)
        {
            Require.NotNull(dictionary, nameof(dictionary));

            Dictionary = dictionary;
        }

        protected Dictionary<T, int> Dictionary { get; }

        public IEnumerator<ScoreEntry<T>> GetEnumerator()
        {
            return Dictionary
                .Select(p => new ScoreEntry<T>(p.Key, p.Value))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IReadOnlyDictionary<T, int> ToReadOnlyDictionary()
        {
            return Dictionary;
        }

        public int Count => Dictionary.Count;
    }
}
