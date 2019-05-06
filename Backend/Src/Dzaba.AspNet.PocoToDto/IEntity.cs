using System;

namespace Dzaba.AspNet.PocoToDto
{
    public interface IEntity<T>
        where T : IEquatable<T>
    {
        T Id { get; set; }
    }
}
