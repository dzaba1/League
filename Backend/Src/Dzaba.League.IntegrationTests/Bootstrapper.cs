using Dzaba.League.DataAccess.Contracts;
using Dzaba.League.DataAccess.EntityFramework;
using Dzaba.League.DataAccess.EntityFramework.Sqlite;
using Dzaba.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Dzaba.League.IntegrationTests
{
    internal static class Bootstrapper
    {
        public static IServiceProvider GetRealContainer()
        {
            var configSection = new Mock<IConfigurationSection>();
            configSection.Setup(x => x["Default"])
                .Returns("ConnectionString");
            var config = new Mock<IConfiguration>();
            config.Setup(x => x.GetSection("ConnectionStrings"))
                .Returns(configSection.Object);

            var collection = new ServiceCollection();
            return new Startup(config.Object).ConfigureServices(collection);
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
