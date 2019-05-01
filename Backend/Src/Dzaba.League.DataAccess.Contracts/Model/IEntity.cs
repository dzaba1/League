using System;

namespace Dzaba.League.DataAccess.Contracts.Model
{
    public interface IEntity<T>
        where T : IEquatable<T>
    {
        T Id { get; set; }
    }
}
