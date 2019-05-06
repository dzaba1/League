using Dzaba.Utils;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Dzaba.AspNet.Auth
{
    public interface IAuthUserDal<TUser, TKey>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
    {
        Task<TUser> GetUserByNameAsync(string email);
        Task<TUser> CreateAsync(string email, string password);
    }

    internal sealed class AuthUserDal<TUser, TKey> : IAuthUserDal<TUser, TKey>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>, new()
    {
        private readonly UserManager<TUser> userManager;

        public AuthUserDal(UserManager<TUser> userManager)
        {
            Require.NotNull(userManager, nameof(userManager));

            this.userManager = userManager;
        }

        public async Task<TUser> CreateAsync(string email, string password)
        {
            Require.NotWhiteSpace(email, nameof(email));
            Require.NotWhiteSpace(password, nameof(password));

            var entity = new TUser
            {
                UserName = email,
                Email = email
            };

            var result = await userManager.CreateAsync(entity);

            if (result.Succeeded)
            {
                return entity;
            }

            throw new IdentityException(result.Errors);
        }

        public Task<TUser> GetUserByNameAsync(string name)
        {
            Require.NotEmpty(name, nameof(name));

            return userManager.FindByNameAsync(name);
        }
    }
}
