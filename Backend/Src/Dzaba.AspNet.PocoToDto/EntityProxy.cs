using System;

namespace Dzaba.AspNet.PocoToDto
{
    public class EntityProxy<T> : IEntity<T>
        where T : IEquatable<T>
    {
        public T Id { get; set; }
    }

    public class NamedEntityProxy<T> : EntityProxy<T>
        where T : IEquatable<T>
    {
        public string Name { get; set; }
    }
}
