using Dzaba.AspNet.Contracts;
using Microsoft.AspNetCore.Identity;
using System;

namespace Dzaba.AspNet.Auth
{
    public interface ITokenGenerator
    {
        TokenData Generate<T>(IdentityUser<T> user) where T : IEquatable<T>;
    }
}
