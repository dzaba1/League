using System;
using Dzaba.AspNet.Contracts;
using Dzaba.Utils;
using Microsoft.AspNetCore.Identity;

namespace Dzaba.AspNet.PocoToDto
{
    public static class Transform
    {
        public static NamedLink<T> ToLink<T>(this IdentityUser<T> user, string url)
            where T : IEquatable<T>
        {
            Require.NotNull(user, nameof(user));
            Require.NotWhiteSpace(url, nameof(url));

            return new NamedLink<T>
            {
                Id = user.Id,
                Name = user.UserName,
                Url = url
            };
        }

        public static NamedEntityProxy<T> ToEntityProxy<T>(this IdentityUser<T> user)
            where T : IEquatable<T>
        {
            Require.NotNull(user, nameof(user));

            return new NamedEntityProxy<T>
            {
                Id = user.Id,
                Name = user.UserName
            };
        }

        public static NamedLink<T> ToLink<T>(this INamedEntity<T> entity, string url)
            where T : IEquatable<T>
        {
            Require.NotNull(entity, nameof(entity));
            Require.NotWhiteSpace(url, nameof(url));

            return new NamedLink<T>
            {
                Id = entity.Id,
                Name = entity.Name,
                Url = url
            };
        }

        public static Link<T> ToLink<T>(this IEntity<T> entity, string url)
            where T : IEquatable<T>
        {
            Require.NotNull(entity, nameof(entity));
            Require.NotWhiteSpace(url, nameof(url));

            return new Link<T>
            {
                Id = entity.Id,
                Url = url
            };
        }
    }
}
