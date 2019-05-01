using Dzaba.League.DataAccess.Contracts;
using Dzaba.League.DataAccess.Contracts.Model;
using Dzaba.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Dzaba.League.DataAccess.EntityFramework
{
    public static class Bootstrapper
    {
        public static void RegisterEntityFrameworkDataAccess(this IServiceCollection container)
        {
            Require.NotNull(container, nameof(container));

            container.AddDbContext<DatabaseContext>(b => OptionsHandler(container, b), ServiceLifetime.Transient, ServiceLifetime.Singleton);
            container.AddTransient<Func<DatabaseContext>>(p => p.GetRequiredService<DatabaseContext>);
            container.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            container.AddTransient<IDbInitializer, DbInitalizer>();
        }

        private static void OptionsHandler(IServiceCollection containerCollection, DbContextOptionsBuilder builder)
        {
            var container = containerCollection.BuildServiceProvider();
            var provider = container.GetRequiredService<IEntityFrameworkProvider>();
            var connectionStringProvider = container.GetRequiredService<IConnectionStringProvider>();

            builder.UseLazyLoadingProxies();
            provider.Register(connectionStringProvider.GetConnectionString(), builder);
        }
    }
}
