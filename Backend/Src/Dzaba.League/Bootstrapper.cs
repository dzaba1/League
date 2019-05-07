using Dzaba.AspNet.Auth;
using Dzaba.League.DataAccess.Contracts;
using Dzaba.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Dzaba.League
{
    public static class Bootstrapper
    {
        public static void RegisterWebApi(this IServiceCollection container)
        {
            Require.NotNull(container, nameof(container));

            container.AddTransient<IConnectionStringProvider, ConnectionStringProvider>();
            container.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .RegisterAuthWebApiPart();
        }
    }
}
