using System;

namespace Dzaba.AspNet.PocoToDto
{
    public interface INamedEntity<T> : IEntity<T>
        where T : IEquatable<T>
    {
        string Name { get; set; }
    }
}
