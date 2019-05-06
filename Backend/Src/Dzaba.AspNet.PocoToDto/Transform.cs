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
    }
}
