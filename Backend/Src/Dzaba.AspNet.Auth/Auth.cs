using System;
using System.Threading.Tasks;
using Dzaba.AspNet.Contracts;
using Dzaba.AspNet.PocoToDto;
using Dzaba.Utils;
using Microsoft.AspNetCore.Identity;

namespace Dzaba.AspNet.Auth
{
    public interface IAuth
    {
        Task<UserInfo<string>> Register(string email, string password);
        Task<UserInfo<string>> Login(string email, string password);
    }

    internal sealed class Auth<TUser, TKey> : IAuth
        where TKey : IEquatable<TKey>, IConvertible
        where TUser : IdentityUser<TKey>
    {
        private readonly ISignInManager<TUser, TKey> signInManager;
        private readonly ITokenGenerator tokenGenerator;
        private readonly IAuthUserDal<TUser, TKey> userDal;

        public Auth(ISignInManager<TUser, TKey> signInManager,
            ITokenGenerator tokenGenerator,
            IAuthUserDal<TUser, TKey> userDal)
        {
            Require.NotNull(signInManager, nameof(signInManager));
            Require.NotNull(tokenGenerator, nameof(tokenGenerator));
            Require.NotNull(userDal, nameof(userDal));

            this.signInManager = signInManager;
            this.tokenGenerator = tokenGenerator;
            this.userDal = userDal;
        }

        public async Task<UserInfo<string>> Login(string email, string password)
        {
            Require.NotEmpty(email, nameof(email));
            Require.NotEmpty(password, nameof(password));

            await signInManager.PasswordSignInAsync(email, password, false);
            var user = await userDal.GetUserByNameAsync(email);
            var token = tokenGenerator.Generate(user);

            return new UserInfo<string>
            {
                User = BuildUserLink(user),
                TokenData = token
            };
        }

        private NamedLink<string> BuildUserLink(TUser user)
        {
            var url = string.Format(Routes.AuthControllerRouteIdFormat, user.Id);
            return new NamedLink<string>
            {
                Id = user.Id.ToString(),
                Name = user.UserName,
                Url = url
            };
        }

        public async Task<UserInfo<string>> Register(string email, string password)
        {
            Require.NotEmpty(email, nameof(email));
            Require.NotEmpty(password, nameof(password));

            var user = await userDal.CreateAsync(email, password);
            await signInManager.SignInAsync(user, false);
            var token = tokenGenerator.Generate(user);

            return new UserInfo<string>
            {
                User = BuildUserLink(user),
                TokenData = token
            };
        }
    }
}
