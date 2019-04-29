using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dzaba.Utils;

namespace Dzaba.League.Algorithms
{
    public abstract class CompetitorsCollectionBase<TId, TValue, TEntry> : IEnumerable<TEntry>
        where TId : IEquatable<TId>
    {
        protected CompetitorsCollectionBase(Dictionary<TId, TValue> dictionary)
        {
            Require.NotNull(dictionary, nameof(dictionary));

            Dictionary = dictionary;
        }

        protected Dictionary<TId, TValue> Dictionary { get; }

        public IEnumerator<TEntry> GetEnumerator()
        {
            return Dictionary
                .Select(BuildFromDictEntry)
                .GetEnumerator();
        }

        protected abstract TEntry BuildFromDictEntry(KeyValuePair<TId, TValue> keyValuePair);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IReadOnlyDictionary<TId, TValue> ToReadOnlyDictionary()
        {
            return Dictionary;
        }

        public int Count => Dictionary.Count;

        public bool ContainsCompetitor(TId competitorId)
        {
            return Dictionary.ContainsKey(competitorId);
        }

        public TValue this[TId competitorId] => Dictionary[competitorId];
    }
}
