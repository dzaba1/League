using System;

namespace Dzaba.League.DataAccess.Contracts.Model
{
    public interface INamedEntity<T> : IEntity<T>
        where T : IEquatable<T>
    {
        string Name { get; set; }
    }
}
