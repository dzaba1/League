﻿using System;
using Dzaba.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace Dzaba.AspNet.Auth
{
    public static class Bootstrapper
    {
        public static void RegisterIdentityAuth<TUser, TKey>(this IServiceCollection container)
            where TKey : IEquatable<TKey>, IConvertible
            where TUser : IdentityUser<TKey>, new()
        {
            Require.NotNull(container, nameof(container));

            container.AddTransient<IAuthUserDal<TUser, TKey>, AuthUserDal<TUser, TKey>>();
            container.AddTransient<ISignInManager<TUser, TKey>, SignInManager<TUser, TKey>>();
            container.AddTransient<IAuth, Auth<TUser, TKey>>();
        }

        public static void RegisterAuthWebApiPart(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddApplicationPart(typeof(Bootstrapper).Assembly)
                .AddControllersAsServices();
        }
    }
}
