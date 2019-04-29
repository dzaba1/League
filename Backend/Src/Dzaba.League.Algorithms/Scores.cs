using System;
using System.Collections.Generic;

namespace Dzaba.League.Algorithms
{
    public class Scores<T> : CompetitorsCollectionBase<T, int, ScoreEntry<T>>
        where T : IEquatable<T>
    {
        public Scores(Dictionary<T, int> dictionary) : base(dictionary)
        {
        }

        protected override ScoreEntry<T> BuildFromDictEntry(KeyValuePair<T, int> keyValuePair)
        {
            return new ScoreEntry<T>(keyValuePair.Key, keyValuePair.Value);
        }
    }
}
