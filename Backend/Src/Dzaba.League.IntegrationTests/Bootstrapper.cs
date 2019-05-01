using Dzaba.League.DataAccess.Contracts;
using Dzaba.League.DataAccess.EntityFramework;
using Dzaba.League.DataAccess.EntityFramework.Sqlite;
using Dzaba.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Dzaba.League.IntegrationTests
{
    internal static class Bootstrapper
    {
        public static IServiceProvider GetRealContainer()
        {
            var collection = new ServiceCollection();
            return new Startup(null).ConfigureServices(collection);
        }

        public static void RegisterIntegrationTests(this IServiceCollection container)
        {
            Require.NotNull(container, nameof(container));

            container.AddTransient<IConnectionStringProvider, IntegrationTestsConnectionStringProvider>();
        }

        public static IServiceProvider GetTestContainer()
        {
            var container = new ServiceCollection();
            container.RegisterEntityFrameworkDataAccess();
            container.RegisterSqlite();
            container.RegisterWebApi();
            container.RegisterIntegrationTests();

            return container.BuildServiceProvider();
        }
    }
}
