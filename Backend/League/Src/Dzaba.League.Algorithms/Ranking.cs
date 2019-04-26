using System;
using System.Collections.Generic;

namespace Dzaba.League.Algorithms
{
    public class Ranking<T> : CompetitorsCollectionBase<T, double, RatingEntry<T>>
        where T : IEquatable<T>
    {
        public Ranking(Dictionary<T, double> dictionary) : base(dictionary)
        {
        }

        protected override RatingEntry<T> BuildFromDictEntry(KeyValuePair<T, double> keyValuePair)
        {
            return new RatingEntry<T>(keyValuePair.Key, keyValuePair.Value);
        }
    }
}
