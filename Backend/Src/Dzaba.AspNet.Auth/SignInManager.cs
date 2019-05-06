using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dzaba.Utils;
using Microsoft.AspNetCore.Identity;

namespace Dzaba.AspNet.Auth
{
    public interface ISignInManager<in TUser, TKey>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
    {
        Task SignInAsync(TUser user, bool isPersistent);
        Task PasswordSignInAsync(string userName, string password, bool isPersistent);
    }

    internal sealed class SignInManager<TUser, TKey> : ISignInManager<TUser, TKey>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
    {
        private readonly SignInManager<TUser> signInManager;

        public SignInManager(SignInManager<TUser> signInManager)
        {
            Require.NotNull(signInManager, nameof(signInManager));

            this.signInManager = signInManager;
        }

        public Task SignInAsync(TUser user, bool isPersistent)
        {
            Require.NotNull(user, nameof(user));

            return signInManager.SignInAsync(user, isPersistent);
        }

        public async Task PasswordSignInAsync(string userName, string password, bool isPersistent)
        {
            Require.NotEmpty(userName, nameof(userName));
            Require.NotEmpty(password, nameof(password));

            var result = await signInManager.PasswordSignInAsync(userName, password, isPersistent, false);

            if (!result.Succeeded)
            {
                throw new IdentityException(BuildErrors(result, userName));
            }
        }

        private IEnumerable<IdentityError> BuildErrors(SignInResult result, string userName)
        {
            if (result.IsLockedOut)
            {
                yield return new IdentityError
                {
                    Code = nameof(SignInResult.IsLockedOut),
                    Description = $"The user {userName} is locked out."
                };
            }

            if (result.IsNotAllowed)
            {
                yield return new IdentityError
                {
                    Code = nameof(SignInResult.IsNotAllowed),
                    Description = $"The user {userName} is not allowed to sign in."
                };
            }

            if (result.RequiresTwoFactor)
            {
                yield return new IdentityError
                {
                    Code = nameof(SignInResult.RequiresTwoFactor),
                    Description = $"The user {userName} requires two factor authentication."
                };
            }
        }
    }
}
